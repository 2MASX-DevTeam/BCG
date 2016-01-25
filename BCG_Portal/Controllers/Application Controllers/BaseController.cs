using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BCG_Portal.Controllers.Application_Controllers
{
    public class BaseController : Controller
    {
        
        public void SendExceptionToAdmin(string ex)
        {
            string emailAdmin = ConfigurationManager.AppSettings["EmailAdministrator"];

            SmtpClient SmtpServer = new SmtpClient();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("no-reply@bc-vit.bg");
            mail.To.Add(emailAdmin);

            mail.Subject = "Exception occured!";
            mail.Body = ex;

            SmtpServer.Send(mail);
        }


        [ChildActionOnly]
        public void SendEmail(string reciever, string subject, string body)
        {

            SmtpClient SmtpServer = new SmtpClient();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("no-reply@bc-vit.bg");
            mail.To.Add(reciever);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpServer.Send(mail);
        }

    }
}