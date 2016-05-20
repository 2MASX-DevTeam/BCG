using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace BCG_Portal.Helpers
{
    public class SendMailHelper
    {
        /* Add the Creddentials- use your own email id and password */
        private string emailFrom = ConfigurationManager.AppSettings["emailFrom"]; //Replace this with your own correct Gmail Address
        private string emailTo = ConfigurationManager.AppSettings["emailTo"]; //Replace this with the Email Address to whom you want to send the mail
        private string credentialsPassword = ConfigurationManager.AppSettings["credentialsPassword"];
        private string emailCredentials = ConfigurationManager.AppSettings["emailCredentials"];
        private string appName = ConfigurationManager.AppSettings["ApplicationName"];
        private SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        private MailMessage mail = new MailMessage();

        /// <summary>
        /// Use this to inform the admins for any issues
        /// </summary>
        /// <param name="message">Represents the error which is throwen by the application</param>
        //public void SendExceptionToAdmin(string message)
        //{
        //    client.Credentials = new System.Net.NetworkCredential(emailCredentials, credentialsPassword);
        //    client.Port = 587; // Gmail works on this port
        //    client.Host = "smtp.gmail.com";
        //    client.EnableSsl = true; //Gmail works on Server Secured Layer

        //    string app = string.Format("{0} Exception Handler", ConfigurationManager.AppSettings["ApplicationName"]);
        //    mail.To.Add(emailTo);
        //    mail.From = new MailAddress(emailFrom, app, System.Text.Encoding.UTF8);
        //    mail.Subject = string.Format("{0} APP Throw an exception!.", ConfigurationManager.AppSettings["ApplicationName"]);
        //    mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //    mail.Body = message;
        //    mail.BodyEncoding = System.Text.Encoding.UTF8;
        //    mail.IsBodyHtml = true;
        //    mail.Priority = MailPriority.High;

        //    TryToSendMail();
        //}

        /// <summary>
        /// Use this to inform the admins for any issues
        /// </summary>
        /// <param name="appName">Current application name. Usefull to recognize from wich application the error is thrown</param>
        //public void SendExceptionToAdmin(string message, string appName)
        //{
        //    client.Credentials = new System.Net.NetworkCredential(emailFrom, credentialsPassword);
        //    client.Port = 587; // Gmail works on this port
        //    client.Host = "smtp.gmail.com";
        //    client.EnableSsl = true; //Gmail works on Server Secured Layer

        //    mail.To.Add(emailTo);
        //    string info = string.Format("{0} Exception Handler", appName);
        //    mail.From = new MailAddress(emailFrom, info, System.Text.Encoding.UTF8);
        //    string subject = string.Format("{0} Throw an exception!.", appName);
        //    mail.Subject = subject;
        //    mail.SubjectEncoding = System.Text.Encoding.UTF8;
        //    mail.Body = message;
        //    mail.BodyEncoding = System.Text.Encoding.UTF8;
        //    mail.IsBodyHtml = true;
        //    mail.Priority = MailPriority.High;

        //    TryToSendMail();
        //}

        internal Task SendEmailConfirmation(IdentityMessage message)
        {
            //client.Credentials = new NetworkCredential(emailCredentials, credentialsPassword);
            //client.Port = 587; // Gmail works on this port
            //client.Host = "smtp.gmail.com";
           // client.EnableSsl = true; //Gmail works on Server Secured Layer

            string app = string.Format("{0} Registration request", appName);
            mail.To.Add(message.Destination.ToString());
            //mail.From = new MailAddress(emailFrom, app, System.Text.Encoding.UTF8);
            mail.From = new MailAddress(emailFrom);
            mail.Subject = string.Format("In answer of your request to register in {0} APP ...", appName);
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = message.Body.ToString();
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            return client.SendMailAsync(mail);
        }

        private void TryToSendMail()
        {
            try
            {
                client.SendMailAsync(mail);
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

               // SendExceptionToAdmin(errorMessage, "TryToSendMail()");
            }
        }

    }
}