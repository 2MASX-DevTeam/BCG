using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Tools;
using Newtonsoft.Json;
//using SendMailHelper;
using BCG_Portal.Models;
using BCG_Portal.Data;

namespace BCG_Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
     //   ExceptionToAdmin mail = new ExceptionToAdmin();

        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

        }

        protected void Session_Start()
        {
            var db = new ApplicationDbContext();
            string ip = Tools.IPAddress.GetClientIPAddress(HttpContext.Current);
            string browserVersion = Request.UserAgent;
            if (!String.IsNullOrWhiteSpace(ip))
            {
                try
                {
                    var fullinfo = GetCountryByIp(ip.ToString());
                    if (fullinfo != null)
                    {
                        var model = db.IPLoginAttempts.Create();
                        {
                            model.IPAdress = ip;
                            model.UserAgend = browserVersion;
                            model.Latitude = fullinfo.latitude;
                            model.Longitude = fullinfo.longitude;
                            model.Country = fullinfo.country_name;
                            model.DateChanged = DateTime.Now;
                            model.DateCreated = DateTime.Now;
                            model.UserName = "SYSTEM";
                        };

                        db.IPLoginAttempts.Add(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    string emailAdmin = ConfigurationManager.AppSettings["EmailAdministrator"];

                    SmtpClient SmtpServer = new SmtpClient();

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("no-reply@bc-vit.bg");
                    mail.To.Add(emailAdmin);

                    mail.Subject = "Exception occured!";
                    mail.Body = ex.ToString();

                    SmtpServer.Send(mail);
                }

            }

        }

        public dynamic GetCountryByIp(string UserIP)
        {
            try
            {
                string url = "http://freegeoip.net/json/" + UserIP;
                WebClient client = new WebClient();
                string jsonstring = client.DownloadString(url);
                dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
                return dynObj;
            }
            catch (Exception)
            {
                //mail.SendExceptionToAdmin(ex.ToString());
                return null;
            }

        }

    }
}
