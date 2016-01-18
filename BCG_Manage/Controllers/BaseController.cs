
namespace BCG_Manage.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using BCG_DB;
    using BCG_DB.Entity.MultiLanguageEntity;
    using PagedList;
    using System.IO;
    using System.Drawing;
    using System.Net.Mail;
    using System.Configuration;
    [Authorize]
    public class BaseController : Controller
    {
        private MultiLanguageModel db = new MultiLanguageModel();

        [ChildActionOnly]
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        [ChildActionOnly]
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        
        public ActionResult ShowImageForLanguages(int id)
        {
            try
            {
                var img = db.tblLanguages.Find(id).Picture;
                if (img == null)
                {
                    var dir = Server.MapPath("/Images");
                    var path = Path.Combine(dir, "no-img" + ".jpg");
                  
                    return base.File(path, "image/jpeg");
                }
                else
                {
                    var imageData = img;
                    return File(imageData, "image/jpeg");
                }
   
            }
            catch (Exception ex)
            {

                SendExceptionToAdmin(ex.ToString());

                var dir = Server.MapPath("/Images");
                var path = Path.Combine(dir, "no-img" + ".jpg");
                return File(path, "image/jpeg");
                
            }



        }

        [ChildActionOnly]
        public void SendExceptionToAdmin(string ex)
        {
            string[] emailsettings;
            string emailAdmin = ConfigurationManager.AppSettings["EmailAdministrator"]; 
#if DEBUG
            emailsettings = System.IO.File.ReadAllLines("E:\\asp.net projects\\BCG\\BCG_Manage\\emailsettings.txt");
            SmtpClient SmtpServer = new SmtpClient("mail.s2kdesign.com ");
            SmtpServer.Port = 26;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailsettings[0], emailsettings[1]);
#else
            SmtpClient SmtpServer = new SmtpClient();
#endif
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
            string[] emailsettings;
#if DEBUG
            emailsettings = System.IO.File.ReadAllLines("E:\\asp.net projects\\BCG\\BCG_Manage\\emailsettings.txt");
            SmtpClient SmtpServer = new SmtpClient("mail.s2kdesign.com");
            SmtpServer.Port = 26;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailsettings[0], emailsettings[1]);
#else
            SmtpClient SmtpServer = new SmtpClient();
#endif
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("no-reply@bc-vit.bg");
            mail.To.Add(reciever);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpServer.Send(mail);
        }

        public int GetAdminCounts()
        {
            return 2;
        }
    }
}