
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

        private void SendExceptionToAdmin(string ex)
        {
            throw new NotImplementedException();
        }
    }
}