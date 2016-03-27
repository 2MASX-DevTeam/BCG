using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace BCG_Portal.Helpers
{
    public class EmailConfirmationHandler
    {
        private string emailFrom = ConfigurationManager.AppSettings["EmailFrom"]; //Replace this with your own correct Gmail Address
        private string credentialsPassword = ConfigurationManager.AppSettings["SmtpCredentialsPass"];
        private string emailTo = ConfigurationManager.AppSettings["EmailTo"]; //Replace this with the Email Address to whom you want to send the mail
        private SmtpClient client = new SmtpClient();
        private MailMessage mail = new MailMessage();

        public void SendEmailConfirmation(string userName, string userEmail, string bodyMsg)
        {
            client.Credentials = new NetworkCredential(emailFrom, credentialsPassword);

            //Add the Creddentials- use your own email id and password
            client.Port = 587; // Gmail works on this port
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Gmail works on Server Secured Layer

            mail.To.Add(userEmail);
            mail.From = new MailAddress(emailFrom, "BC-VIT: Your Bussiness Card Creator", System.Text.Encoding.UTF8);
            mail.Subject = "Confirmation e-mail";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = bodyMsg;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            client.Send(mail);
        }

    }
}