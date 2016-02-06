
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
    using Areas.EmailTemplates.Controllers;
    using System.Web.Security;
    using Models;
    using System.Net;
    using Newtonsoft.Json;
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
                  
                    return File(path, "image/jpeg");
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

        [ChildActionOnly]
        public string GenericSymbols()
        {
            string Template = ConfigurationManager.AppSettings["passwordTemplate"].ToString();
            String l = "qwertyuiopasdfghjklzxcvbnm";
            String n = "0123456789";
            String p = ",.?<>;:@/!\"%^&*()-+.=_";
            String L = "QWERTYUIOPASDFGHJKLZXCVBNM";
            String c = "qwrtypsdfghjklzxcvbnm";
            String C = "QWRTYPSDFGHJKLZXCVBNM";
            String v = "aeiou";
            String V = "AEIOU";
            String a = "qwertyuiopasdfghjkl.zxcvbnmQWERTYUIOPASD.FGHJKLZXCVBNM,.?<>;:.@/!\"%^&*()-+=_012345.6789";

            Int32 lenTempalte = Template.Length;
            String newPassword = String.Empty;

            Random randPassword = new Random();
            for (Int32 Index = 0; Index < lenTempalte; Index++)
            {
                if (Template[Index] == 'l') { newPassword += l[randPassword.Next(0, l.Length)]; }
                if (Template[Index] == 'L') { newPassword += L[randPassword.Next(0, L.Length)]; }
                if (Template[Index] == 'c') { newPassword += c[randPassword.Next(0, c.Length)]; }
                if (Template[Index] == 'C') { newPassword += C[randPassword.Next(0, C.Length)]; }
                if (Template[Index] == 'v') { newPassword += v[randPassword.Next(0, v.Length)]; }
                if (Template[Index] == 'V') { newPassword += V[randPassword.Next(0, V.Length)]; }
                if (Template[Index] == 'n') { newPassword += n[randPassword.Next(0, n.Length)]; }
                if (Template[Index] == 'p') { newPassword += p[randPassword.Next(0, p.Length)]; }
                if (Template[Index] == 'a') { newPassword += a[randPassword.Next(0, a.Length)]; }
                newPassword += Template[Index];
            }

            return newPassword;
        }

        public static string GenericRandomNumbers(Int32 CountNumbers)
        {
            String Numbers = "0123456789";
            String genNumbers = String.Empty;

            Random randNumbers = new Random();
            for (Int32 Index = 0; Index < CountNumbers; Index++)
                genNumbers += Numbers[randNumbers.Next(0, Numbers.Length)];

            return genNumbers;
        }


        public string RenderViewToString(string TemplateName, object model)
        {
            TemplateName = "~/Areas/EmailTemplates/Views/Email/" + TemplateName + ".cshtml";
           // var controller = new EmailController();
            
            ViewData.Model = model;

            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, TemplateName, null);
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultErrors"] = "There was a problem with rendering template for email!";
                return "Error in register form! Email with the problem was send to aministrator.";
            }
        }

     
    }
}