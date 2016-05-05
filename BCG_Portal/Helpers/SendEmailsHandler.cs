using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BCG_Portal.Helpers
{
    public class SendEmailsHandler
    {
        private string emailFrom = ConfigurationManager.AppSettings["EmailFrom"]; //Replace this with your own correct Gmail Address
        private string credentialsPassword = ConfigurationManager.AppSettings["SmtpCredentialsPass"];
        private string emailTo = ConfigurationManager.AppSettings["EmailTo"]; //Replace this with the Email Address to whom you want to send the mail
        private SmtpClient client = new SmtpClient();
        private MailMessage mail = new MailMessage();

        /// <summary>
        /// Use this to inform the admins for any issues
        /// </summary>
        /// <param name="message">Represents the error which is throwen by the application</param>
        public void SendExceptionToAdmin(string message)
        {
            client.Credentials = new System.Net.NetworkCredential(emailFrom, credentialsPassword);
            client.Port = 587; // Gmail works on this port
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Gmail works on Server Secured Layer

            mail.To.Add(emailTo);
            mail.From = new MailAddress(emailFrom, "BCG Exception Handler", System.Text.Encoding.UTF8);
            mail.Subject = "BCG APP Throw an exception!.";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            TryToSendMail();
        }

        /// <summary>
        /// Use this to inform the admins for any issues
        /// </summary>
        /// <param name="appName">Current application name. Usefull to recognize from wich application the error is thrown</param>
        public void SendExceptionToAdmin(string message, string appName)
        {
            client.Credentials = new System.Net.NetworkCredential(emailFrom, credentialsPassword);
            client.Port = 587; // Gmail works on this port
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true; //Gmail works on Server Secured Layer

            mail.To.Add(emailTo);
            string info = string.Format("{0} Exception Handler", appName);
            mail.From = new MailAddress(emailFrom, info, System.Text.Encoding.UTF8);
            string subject = string.Format("{0} Throw an exception!.", appName);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            TryToSendMail();
        }

        private void TryToSendMail()
        {
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += (ex2.ToString() + " ");
                    ex2 = ex2.InnerException;
                }

                SendExceptionToAdmin(errorMessage, "TryToSendMail()");
            }
        }
    }
}