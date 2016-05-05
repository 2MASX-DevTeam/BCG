using BCG_DB.Entity.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;

namespace BCG_Manage
{
    public class MvcApplication : System.Web.HttpApplication
    {

        private LoginAttempts db = new LoginAttempts();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            string ip = Tools.IPAddress.GetClientIPAddress(System.Web.HttpContext.Current);
            string browserVersion = Request.UserAgent;
            if (!String.IsNullOrWhiteSpace(ip))
            {
                try
                {
                    var fullinfo = GetCountryByIp(ip.ToString());
                    if (fullinfo != null)
                    {
                        var model = new tblIPLoginAttempts()
                        {
                            IPAdress = ip,
                            UserAgend = browserVersion,
                            Latitude = fullinfo.latitude,
                            Longitude = fullinfo.longitude,
                            Country = fullinfo.country_name,
                            DateChanged = DateTime.Now,
                            DateCreated = DateTime.Now,
                            UserName = "SYSTEM"
                        };

                        db.tblIPLoginAttempts.Add(model);
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
            catch (Exception ex)
            {
                // SendExceptionToAdmin(ex.ToString());
                return null;
            }

        }

    }
}
