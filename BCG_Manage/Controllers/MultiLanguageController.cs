
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
    using System.Data.Entity;

    [Authorize]
    public class MultiLanguageController : BaseController
    {
        public byte[] Content { get; set; }

        private MultiLanguageModel db = new MultiLanguageModel();

        #region View All


        public ActionResult ViewAllLanguages()
        {
            return View(db);
        }

        #endregion

        #region Add


        // GET: MultiLanguage
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddNewLanguage()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewLanguage(string Language, string Initials, string Culture, bool IsActive, HttpPostedFileBase file)
        {
            if (String.IsNullOrWhiteSpace(Language))
            {
                ViewBag.Result = "Language is Required!";
                return View();
            }

            if (String.IsNullOrWhiteSpace(Initials))
            {
                ViewBag.Result = "Initials is Required!";
                return View();
            }

            if (String.IsNullOrWhiteSpace(Culture))
            {
                ViewBag.Result = "Culture is Required!";
                return View();
            }
            if (file == null)
            {
                ViewBag.Result = "File is Required!";
                return View();
            }

            string filename = Path.GetExtension(file.FileName);
            if (filename == ".jpg" || filename == ".png" || filename == ".gif")
            {
                ViewBag.Result = "Succes!";

                var image = Image.FromStream(file.InputStream, true, true);
                Content = imageToByteArray(image);

                var tbl = new tblLanguages() {
                    Language = Language,
                    Initials = Initials,
                    Culture = Culture,
                    IsActive = IsActive,
                    Picture = Content,
                    Datechanged = DateTime.Now,
                    DateCreated = DateTime.Now
                };

                db.tblLanguages.Add(tbl);
                db.SaveChanges();

                return RedirectToAction("AddNewLanguage");
            }

        

            return View();
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult EditLanguage(int id)
        {
            var model = db.tblLanguages.Find(id);


            return View(model);
        }

        [HttpPost]
        public ActionResult EditLanguage(int id, string Language, string Initials, string Culture, bool IsActive, HttpPostedFileBase file)
        {
            if (id == 0)
            {
                ViewBag.Result = "Error occured!";
                return View();
            }

            if (String.IsNullOrWhiteSpace(Language))
            {
                ViewBag.Result = "Language is Required!";
                return View();
            }

            if (String.IsNullOrWhiteSpace(Initials))
            {
                ViewBag.Result = "Initials is Required!";
                return View();
            }

            if (String.IsNullOrWhiteSpace(Culture))
            {
                ViewBag.Result = "Culture is Required!";
                return View();
            }
            if (file == null)
            {
                ViewBag.Result = "File is Required!";
                return View();
            }

            string filename = Path.GetExtension(file.FileName);
            if (filename == ".jpg" || filename == ".png" || filename == ".gif")
            {
                ViewBag.Result = "Succes!";

                var image = Image.FromStream(file.InputStream, true, true);
                Content = imageToByteArray(image);

                var tbl = new tblLanguages()
                {
                    Language = Language,
                    Initials = Initials,
                    Culture = Culture,
                    IsActive = IsActive,
                    Picture = Content,
                    Datechanged = DateTime.Now
                };
                
                db.Entry(tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditLanguage", new { id = id });
            }
            

            return View();
        }
        #endregion

        public ActionResult Show(int id)
        {
            var img = db.tblLanguages.Find(id).Picture;
            var imageData = img;

            return File(imageData, "image/jpg");
        }

    }
}