
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
    using Models;

    [Authorize]
    public class MultiLanguageController : BaseController
    {
        public byte[] Content { get; set; }

        private MultiLanguageModel db = new MultiLanguageModel();


        #region Languages

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
        public ActionResult AddNewLanguage(LanguagesInModels model)
        {

            if (ModelState.IsValid)
            {

                if (model.Picture != null)
                {
                    string filename = Path.GetExtension(model.Picture.FileName);
                    if (filename == ".jpg" || filename == ".png" || filename == ".gif")
                    {
                        TempData["ResultSuccess"] = "Succes in adding new language!";
                        var image = Image.FromStream(model.Picture.InputStream, true, true);
                        Content = imageToByteArray(image);

                        var tbl = new tblLanguages()
                        {
                            Language = model.Language,
                            Initials = model.Initials,
                            Culture = model.Culture,
                            IsActive = model.IsActive,
                            Picture = Content,
                            Datechanged = DateTime.Now,
                            DateCreated = DateTime.Now
                        };

                        db.tblLanguages.Add(tbl);
                        db.SaveChanges();

                        return RedirectToAction("AddNewLanguage");
                    }
                }
                else
                {
                    TempData["ResultSuccess"] = "Succes in adding new language!";
    
                    var tbl = new tblLanguages()
                    {
                        Language = model.Language,
                        Initials = model.Initials,
                        Culture = model.Culture,
                        IsActive = model.IsActive,
                        Datechanged = DateTime.Now,
                        DateCreated = DateTime.Now
                    };

                    db.tblLanguages.Add(tbl);
                    db.SaveChanges();

                    return RedirectToAction("AddNewLanguage");
                }
            }
            else
            {
                TempData["ResultError"] = "Error occured in adding new language!";
            }

            return View(model);
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult EditLanguage(int id)
        {
            var model = db.tblLanguages.Find(id);
            var newmodel = new LanguagesInModels();
            newmodel.IdLanguage = model.IdLanguage;
            newmodel.Initials = model.Initials;
            newmodel.IsActive = model.IsActive;
            newmodel.Language = model.Language;
            newmodel.Culture = model.Culture;
            return View(newmodel);
        }

        [HttpPost]
        public ActionResult EditLanguage(LanguagesInModels model)
        {
            if (ModelState.IsValid)
            {

                if (model.Picture != null)
                {
                    string filename = Path.GetExtension(model.Picture.FileName);
                    if (filename == ".jpg" || filename == ".png" || filename == ".gif")
                    {
                        TempData["ResultSuccess"] = "Successfully updating language!";

                        var image = Image.FromStream(model.Picture.InputStream, true, true);
                        Content = imageToByteArray(image);

                        var tbl = db.tblLanguages.Find(model.IdLanguage);

                        tbl.Language = model.Language;
                        tbl.Initials = model.Initials;
                        tbl.Culture = model.Culture;
                        tbl.IsActive = model.IsActive;
                        tbl.Picture = Content;
                        tbl.UserName = User.Identity.Name;
                        tbl.Datechanged = DateTime.Now;

                        db.SaveChanges();
                        return View(model);
                    }
                }
                else
                {
                    TempData["ResultSuccess"] = "Successfully updating language!";
                    var tbl = db.tblLanguages.Find(model.IdLanguage);

                    tbl.Language = model.Language;
                    tbl.Initials = model.Initials;
                    tbl.Culture = model.Culture;
                    tbl.IsActive = model.IsActive;
                    tbl.UserName = User.Identity.Name;
                    tbl.Datechanged = DateTime.Now;

                    db.SaveChanges();
                    return View(model);
                }
            }
            else
            {
                TempData["ResultError"] = "Error occured in updating language!";
            }

            return View(model);
        }
        #endregion


        #region Update 

        public ActionResult ChangeIsActiveLanguage(int id)
        {
            var tbl = db.tblLanguages.Find(id);

            tbl.IsActive = (tbl.IsActive) ? false : true;
            tbl.Datechanged = DateTime.Now;
            tbl.UserName = User.Identity.Name;

            db.SaveChanges();
            return RedirectToAction("ViewAllLanguages");
        }
        #endregion

        #endregion


        #region Resources

        [HttpGet]
        public ActionResult ViewAllResources()
        {
            return View(db.tblResources);
        }


        [HttpGet]
        public ActionResult AddNewResource()
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult EditResource()
        {
            return View();
        }

        #endregion
    }
}