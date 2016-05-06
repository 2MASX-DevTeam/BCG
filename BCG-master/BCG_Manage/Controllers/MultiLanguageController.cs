
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
    using System.Configuration;
    using System.Data.SqlClient;

    [Authorize]
    public class MultiLanguageController : BaseController
    {
        private byte[] Content;

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
                    TempData["ResultSuccess"] = "Success in adding new language!";

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
        [ValidateAntiForgeryToken]
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
        public ActionResult ViewAllResources(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdResSortParm = String.IsNullOrEmpty(sortOrder) ? "IdResource" : "";
            ViewBag.IdLangSortParm = sortOrder == "IdLanguage" ? "IdLanguageDesc" : "IdLanguage";
            ViewBag.ContextSortParm = sortOrder == "Context" ? "ContextDesc" : "Context";
            ViewBag.DateModifySortParm = sortOrder == "DateChanged" ? "DateChangedDesc" : "DateChanged";
            ViewBag.DateCreatedSortParm = sortOrder == "DateCreated" ? "DateCreatedDesc" : "DateCreated";

            if (searchString != null)
                page = 1;

            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var tblRes = db.tblResources.Include(t => t.tblContext).Include(t => t.tblLanguages);

            if (!String.IsNullOrEmpty(searchString))
                tblRes = tblRes.Where(s => s.tblContext.Context.Contains(searchString));

            switch (sortOrder)
            {
                case "IdResource":
                    tblRes = tblRes.OrderByDescending(s => s.IdResource); break;
                case "IdLanguage":
                    tblRes = tblRes.OrderBy(s => s.IdLanguage); break;
                case "IdLanguageDesc":
                    tblRes = tblRes.OrderByDescending(s => s.IdLanguage); break;
                case "Context":
                    tblRes = tblRes.OrderBy(s => s.tblContext.Context); break;
                case "ContextDesc":
                    tblRes = tblRes.OrderByDescending(s => s.tblContext.Context); break;
                case "DateChanged":
                    tblRes = tblRes.OrderBy(s => s.DateChanged); break;
                case "DateChangedDesc":
                    tblRes = tblRes.OrderByDescending(s => s.DateChanged); break;
                case "DateCreated":
                    tblRes = tblRes.OrderBy(s => s.DateCreated); break;
                case "DateCreatedDesc":
                    tblRes = tblRes.OrderByDescending(s => s.DateCreated); break;


                default:
                    tblRes = tblRes.OrderBy(s => s.IdResource);
                    break;
            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = (page ?? 1);


            return View(tblRes.ToPagedList(pageNumber, pageSize));
        }

        #region Add

        [HttpGet]
        public ActionResult AddNewResource()
        {
            var model = new Resources()
            {
                IdLanguages = new SelectList(db.tblLanguages, "IdLanguage", "Language")
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewResource(Resources model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["ResultSuccess"] = "Succes in adding new resource!";

                    using (var dataContext = new MultiLanguageModel())
                    using (var transaction = dataContext.Database.BeginTransaction())
                    {
                        var tblCon = new tblContext();
                        tblCon.Context = model.Context;
                        tblCon.UserName = User.Identity.Name;
                        tblCon.DateChanged = DateTime.Now;
                        tblCon.DateCreated = DateTime.Now;
                        db.tblContext.Add(tblCon);
                        db.SaveChanges();

                        var IdCont = (from tblContext in db.tblContext
                                      orderby tblContext.DateCreated descending
                                      select tblContext).First();
                        int intIdt = db.tblResources.Max(u => u.IdResource);
                        var tblRes = new tblResources()
                        {
                            IdResource = intIdt + 1,
                            IdLanguage = Convert.ToInt32(model.IdLanguage),
                            IdContext = IdCont.IdContext,
                            UserName = User.Identity.Name,
                            DateChanged = DateTime.Now,
                            DateCreated = DateTime.Now
                        };

                        dataContext.Set<tblResources>().Add(tblRes);
                        dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblResources ON");
                        dataContext.SaveChanges();
                        dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblResources OFF");

                        transaction.Commit();
                    }
                    return RedirectToAction("ViewAllResources");
                }
                catch (Exception ex)
                {
                    SendExceptionToAdmin(ex.ToString());
                }

            }
            else
                TempData["ResultError"] = "Error in adding new translation!";

            return RedirectToAction("ViewAllResources");
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult AddTranslationResource(int id)
        {
            int IdRes = id;
            var model = new Resources();
            try
            {
                var query = @"SELECT IdLanguage, Language FROM tblLanguages
                       WHERE tblLanguages.IdLanguage NOT IN(
                    SELECT  tblLanguages.IdLanguage FROM tblLanguages INNER JOIN
                            tblResources ON tblLanguages.IdLanguage = tblResources.IdLanguage
                    WHERE   tblResources.IdResource =" + IdRes + " )";

                var sequenceQueryResult = db.Database.SqlQuery<Resources>(query).ToList();

                var lsContext = (from tblResources in db.tblResources
                                 where tblResources.IdResource == IdRes
                                 select tblResources.tblContext.Context
                                ).ToList();


                if (sequenceQueryResult.Count != 0)
                {
                    model.IdResource = IdRes;
                    model.IdLanguages = new SelectList(sequenceQueryResult, "IdLanguage", "Language");
                    model.lsContext = lsContext;

                }
                else
                {
                    TempData["ResultAlert"] = "There is no more languages.";
                    return RedirectToAction("ViewAllResources");
                }
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTranslationResource(Resources model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["ResultSuccess"] = "Succes in adding new resource!";

                    using (var dataContext = new MultiLanguageModel())
                    using (var transaction = dataContext.Database.BeginTransaction())
                    {
                        var tblCon = new tblContext();
                        tblCon.Context = model.Context;
                        tblCon.UserName = User.Identity.Name;
                        tblCon.DateChanged = DateTime.Now;
                        tblCon.DateCreated = DateTime.Now;
                        db.tblContext.Add(tblCon);
                        db.SaveChanges();

                        var IdCont = (from tblContext in db.tblContext
                                      orderby tblContext.DateCreated descending
                                      select tblContext).First();

                        var tblRes = new tblResources()
                        {
                            IdResource = model.IdResource,
                            IdLanguage = Convert.ToInt32(model.IdLanguage),
                            IdContext = IdCont.IdContext,
                            UserName = User.Identity.Name,
                            DateChanged = DateTime.Now,
                            DateCreated = DateTime.Now
                        };

                        dataContext.Set<tblResources>().Add(tblRes);
                        dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblResources ON");
                        dataContext.SaveChanges();
                        dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblResources OFF");

                        transaction.Commit();
                    }
                    return RedirectToAction("AddTranslationResource", new { id = model.IdResource });
                }
                catch (Exception ex)
                {
                    SendExceptionToAdmin(ex.ToString());
                }
            }

            else
                TempData["ResultError"] = "Error in adding new translation!";



            return RedirectToAction("AddTranslationResource");
        }
        #endregion

        #region Edit

        [HttpGet]
        public ActionResult EditResource(int id, int idLang, int idCont)
        {
            var model = new Resources();
            try
            {
                var tbl = db.tblResources.Find(id, idLang, idCont);

                model.IdResource = tbl.IdResource;
                model.IdContext = tbl.IdContext;
                model.IdLanguage = tbl.IdLanguage;
                model.Language = tbl.tblLanguages.Language;
                model.Context = tbl.tblContext.Context;

            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in loading for editing resource!";
                return RedirectToAction("ViewAllStaticTexts");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditResource(Resources model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tbl = db.tblResources.Find(model.IdResource, model.IdLanguage, model.IdContext);

           
                    tbl.tblContext.Context = model.Context;
                    tbl.DateChanged = DateTime.Now;
                    tbl.tblContext.DateChanged = DateTime.Now;

                    db.SaveChanges();
                    TempData["ResultSuccess"] = "Success in edditing resource";
                    return RedirectToAction("ViewAllResources");
                }
                catch (Exception ex)
                {
                    SendExceptionToAdmin(ex.ToString());
                    TempData["ResultError"] = "Error in updating  text!";
                    return RedirectToAction("ViewAllResources");
                }

            }
            TempData["ResultError"] = "Error in updating text!";
            return RedirectToAction("EditResource");
        }

        #endregion

        #region DeleteResource

        public ActionResult DeleteResource(int id, int idLang, int idCont)
        {
            try
            {

                var tbl = db.tblResources.Find(id, idLang, idCont);
                db.tblResources.Remove(tbl);
                db.SaveChanges();
                TempData["ResultSuccess"] = "Success in deleting static text!";
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in loading for editing  static text!";

            }


            return RedirectToAction("ViewAllResources");
        }


        #endregion


        #endregion


        #region StaticResources

        #region View All


        [HttpGet]
        public ActionResult ViewAllStaticTexts(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdResSortParm = String.IsNullOrEmpty(sortOrder) ? "IdStaticResource" : "";
            ViewBag.IdLangSortParm = sortOrder == "IdLanguage" ? "IdLanguageDesc" : "IdLanguage";
            ViewBag.ContextSortParm = sortOrder == "Context" ? "ContextDesc" : "Context";
            ViewBag.DateModifySortParm = sortOrder == "DateChanged" ? "DateChangedDesc" : "DateChanged";
            ViewBag.DateCreatedSortParm = sortOrder == "DateCreated" ? "DateCreatedDesc" : "DateCreated";

            if (searchString != null)
                page = 1;

            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var tblStatic = db.tblStaticResources.Include(t => t.tblStaticTexts).Include(t => t.tblLanguages);

            if (!String.IsNullOrEmpty(searchString))
                tblStatic = tblStatic.Where(s => s.tblStaticTexts.StaticText.Contains(searchString));

            switch (sortOrder)
            {
                case "IdStaticResource":
                    tblStatic = tblStatic.OrderByDescending(s => s.IdStatic); break;
                case "IdLanguage":
                    tblStatic = tblStatic.OrderBy(s => s.IdLanguage); break;
                case "IdLanguageDesc":
                    tblStatic = tblStatic.OrderByDescending(s => s.IdLanguage); break;
                case "Context":
                    tblStatic = tblStatic.OrderBy(s => s.tblStaticTexts.StaticText); break;
                case "ContextDesc":
                    tblStatic = tblStatic.OrderByDescending(s => s.tblStaticTexts.StaticText); break;
                case "DateChanged":
                    tblStatic = tblStatic.OrderBy(s => s.DateChanged); break;
                case "DateChangedDesc":
                    tblStatic = tblStatic.OrderByDescending(s => s.DateChanged); break;
                case "DateCreated":
                    tblStatic = tblStatic.OrderBy(s => s.DateCreated); break;
                case "DateCreatedDesc":
                    tblStatic = tblStatic.OrderByDescending(s => s.DateCreated); break;


                default:
                    tblStatic = tblStatic.OrderBy(s => s.IdStatic);
                    break;
            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = (page ?? 1);


            return View(tblStatic.ToPagedList(pageNumber, pageSize));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetStaticText(int IdStaic, int IdLang, int IdStaticText)
        {

            var model = new StaticDetails();


            var tblStatic = db.tblStaticResources
                .Include(t => t.tblStaticTexts)
                .Include(t => t.tblLanguages)
                .Where(t => t.IdStatic == IdStaic)
                .Where(s => s.IdLanguage == IdLang)
                .Where(s => s.IdStaticText == IdStaticText)
                .Select(s => new { s.tblStaticTexts.StaticText, s.Description }).ToList();

            model.Description = tblStatic[0].Description;
            model.StaticText = tblStatic[0].StaticText;

            return Json(model);
        }

        #endregion


        #region Add


        [HttpGet]
        public ActionResult AddNewStaticText()
        {
            var model = new StaticResources()
            {
                IdLanguages = new SelectList(db.tblLanguages, "IdLanguage", "Language")
            };


            return View(model);
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewStaticText(StaticResources model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dataContext = new MultiLanguageModel())
                    using (var transaction = dataContext.Database.BeginTransaction())
                    {
                        var tblCon = new tblStaticTexts();
                        tblCon.StaticText = model.Text;
                        tblCon.UserName = User.Identity.Name;
                        tblCon.DateChanged = DateTime.Now;
                        tblCon.DateCreated = DateTime.Now;

                        db.tblStaticTexts.Add(tblCon);
                        db.SaveChanges();

                        var IdCont = (from tblStaticTexts in db.tblStaticTexts
                                      orderby tblStaticTexts.DateCreated descending
                                      select tblStaticTexts).First();
                        int intIdt = db.tblStaticResources.Max(u => u.IdStatic);
                        var tblRes = new tblStaticResources()
                        {
                            IdStatic = intIdt + 1,
                            IdLanguage = Convert.ToInt32(model.IdLanguage),
                            IdStaticText = IdCont.IdStaticText,
                            Description = model.Description,
                            UserName = User.Identity.Name,
                            DateChanged = DateTime.Now,
                            DateCreated = DateTime.Now
                        };

                        dataContext.Set<tblStaticResources>().Add(tblRes);
                        dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblStaticResources ON");
                        dataContext.SaveChanges();
                        dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblStaticResources OFF");

                        transaction.Commit();
                        TempData["ResultSuccess"] = "Succes in adding new resource!";
                    }
                    return RedirectToAction("ViewAllStaticTexts");
                }
                catch (Exception ex)
                {
                    SendExceptionToAdmin(ex.ToString());
                }

            }
            else
                TempData["ResultError"] = "Error in adding new translation!";

            model.IdLanguages = new SelectList(db.tblLanguages, "IdLanguage", "Language");
            return View(model);

        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult AddTranslationStaticText(int id)
        {
            int IdStatRes = id;
            var model = new StaticResources();
            try
            {
                var query = @"SELECT IdLanguage, Language FROM tblLanguages
                       WHERE tblLanguages.IdLanguage NOT IN(
                    SELECT  tblLanguages.IdLanguage FROM tblLanguages INNER JOIN
                            tblStaticResources ON tblLanguages.IdLanguage = tblStaticResources.IdLanguage
                    WHERE   tblStaticResources.IdStatic =" + IdStatRes + " )";

                var sequenceQueryResult = db.Database.SqlQuery<StaticResources>(query).ToList();

                var lsContext = (from tblStaticResources in db.tblStaticResources
                                 where tblStaticResources.IdStatic == IdStatRes
                                 select tblStaticResources.Description
                                ).ToList();


                if (sequenceQueryResult.Count != 0)
                {
                    model.IdStaticResource = IdStatRes;
                    model.IdLanguages = new SelectList(sequenceQueryResult, "IdLanguage", "Language");
                    model.lsDescription = lsContext;

                }
                else
                {
                    TempData["ResultAlert"] = "There is no more languages.";
                    return RedirectToAction("ViewAllResources");
                }
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTranslationStaticText(StaticResources model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var dataContext = new MultiLanguageModel())
                    using (var transaction = dataContext.Database.BeginTransaction())
                    {
                        var tblCon = new tblStaticTexts();
                        tblCon.StaticText = model.Text;
                        tblCon.UserName = User.Identity.Name;
                        tblCon.DateChanged = DateTime.Now;
                        tblCon.DateCreated = DateTime.Now;
                        db.tblStaticTexts.Add(tblCon);
                        db.SaveChanges();

                        var IdCont = (from tblStaticTexts in db.tblStaticTexts
                                      orderby tblStaticTexts.DateCreated descending
                                      select tblStaticTexts).First();

                        var tblRes = new tblStaticResources()
                        {
                            IdStatic = model.IdStaticResource,
                            IdLanguage = Convert.ToInt32(model.IdLanguage),
                            IdStaticText = IdCont.IdStaticText,
                            Description = model.Description,
                            UserName = User.Identity.Name,
                            DateChanged = DateTime.Now,
                            DateCreated = DateTime.Now
                        };

                        dataContext.Set<tblStaticResources>().Add(tblRes);
                        dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblStaticResources ON");
                        dataContext.SaveChanges();
                        dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblStaticResources OFF");

                        transaction.Commit();
                        TempData["ResultSuccess"] = "Succes in adding new resource!";

                    }
                    return RedirectToAction("AddTranslationStaticText", new { id = model.IdStaticResource });

                }
                catch (Exception ex)
                {
                    TempData["ResultError"] = "Error in adding new translation!";
                    SendExceptionToAdmin(ex.ToString());
                }
            }

            else
                TempData["ResultError"] = "Error in adding new translation!";
            return RedirectToAction("AddTranslationStaticText");
        }

        #endregion

        #region Edit


        [HttpGet]
        public ActionResult EditStaticText(int id, int idLang, int idCont)
        {
            var model = new StaticResources();

            try
            {

                var tblStatic = db.tblStaticResources
                    .Include(t => t.tblStaticTexts)
                    .Include(t => t.tblLanguages)
                    .Where(t => t.IdStatic == id)
                    .Where(s => s.IdLanguage == idLang)
                    .Where(s => s.IdStaticText == idCont)
                    .Select(s => new { s.tblStaticTexts.StaticText, s.Description, s.IdLanguage, s.tblLanguages.Language }).ToList();

                model.Description = tblStatic[0].Description;
                model.Text = tblStatic[0].StaticText;
                model.Language = tblStatic[0].Language;
                model.IdLanguage = tblStatic[0].IdLanguage;
                model.idStaticText = idCont;
                model.IdStaticResource = id;
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in loading for editing  static text!";
                return RedirectToAction("ViewAllStaticTexts");
            }


            return View(model);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult EditStaticText(StaticResources model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tbl = db.tblStaticResources.Find(model.IdStaticResource, model.IdLanguage, model.idStaticText);

                    tbl.Description = model.Description;
                    tbl.tblStaticTexts.StaticText = model.Text;
                    tbl.DateChanged = DateTime.Now;
                    tbl.tblStaticTexts.DateChanged = DateTime.Now;

                    db.SaveChanges();
                    TempData["ResultSuccess"] = "Success in edditing static resource";
                    return RedirectToAction("ViewAllStaticTexts");
                }
                catch (Exception ex)
                {
                    SendExceptionToAdmin(ex.ToString());
                    TempData["ResultError"] = "Error in updating  static text!";
                    return RedirectToAction("ViewAllStaticTexts");
                }

            }
            TempData["ResultError"] = "Error in updating  static text!";
            return View(model);
        }

        #endregion

        #region Delete

        public ActionResult DeleteStaticText(int id, int idLang, int idCont)
        {
            try
            {

                var tbl = db.tblStaticResources.Find(id, idLang, idCont);
                db.tblStaticResources.Remove(tbl);
                db.SaveChanges();
                TempData["ResultSuccess"] = "Success in deleting static text!";
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in loading for editing  static text!";

            }


            return RedirectToAction("ViewAllStaticTexts");
        }


        #endregion



        #endregion
    }
}