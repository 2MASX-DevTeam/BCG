
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
        private byte[] Content { get; set; }

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
                TempData["ResultSuccess"] = "Succes in adding new resource!";
                var tblRes = new tblResources()
                {
                    IdLanguage = Convert.ToInt32(model.IdLanguage),
                    tblContext = new tblContext
                    {
                        Context = model.Context,
                        UserName = User.Identity.Name,
                        DateChanged = DateTime.Now,
                        DateCreated = DateTime.Now
                    },
                    UserName = User.Identity.Name,
                    DateChanged = DateTime.Now,
                    DateCreated = DateTime.Now
                };

                db.tblResources.Add(tblRes);
                db.SaveChanges();

                return RedirectToAction("AddNewResource");
            }
            else
                TempData["ResultError"] = "Error in adding new resource!";

            return RedirectToAction("AddNewResource");
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult AddTranslationResource(int id)
        {
            int IdRes = id;

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

            var model = new Resources();
            if (sequenceQueryResult.Count != 0)
            {
                model.IdResource = IdRes;
                model.IdLanguages = new SelectList(sequenceQueryResult, "IdLanguage", "Language");
                model.lsContext = lsContext;
            }
            else
            {
                TempData["ResultError"] = "Error in adding new translation to resource! There is no more languages.";
                return RedirectToAction("ViewAllResources");
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTranslationResource(Resources model)
        {
            if (ModelState.IsValid)
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
            else
                TempData["ResultError"] = "Error in adding new translation!";
            return RedirectToAction("AddTranslationResource");
        }
        #endregion


        [HttpGet]
        public ActionResult EditResource()
        {
            return View();
        }

        #endregion

        #region StaticResources


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
                case "IdResource":
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
            try
            {
                if (ModelState.IsValid)
                {
                    var tblStatRes = new tblStaticResources()
                    {
                        IdLanguage = Convert.ToInt32(model.IdLanguage),
                        tblStaticTexts = new tblStaticTexts
                        {
                            StaticText = model.Text,
                            UserName = User.Identity.Name,
                            DateChanged = DateTime.Now,
                            DateCreated = DateTime.Now
                        },
                        Description = model.Description,
                        StaticName = model.Description,
                      
                        UserName = User.Identity.Name,
                        DateChanged = DateTime.Now,
                        DateCreated = DateTime.Now
                    };
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblResources ON");
                    db.tblStaticResources.Add(tblStatRes);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT tblResources OFF");

                    TempData["ResultSuccess"] = "Succes in adding new resource!";
                    return RedirectToAction("AddNewStaticText");
                }
                else
                {
                    TempData["ResultError"] = "Error in adding new resource!";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in adding new resource!";
                return RedirectToAction("AddNewStaticText");

            }

        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult AddTranslationStaticText(int id)
        {
            int IdRes = id;

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

            var model = new Resources();
            if (sequenceQueryResult.Count != 0)
            {
                model.IdResource = IdRes;
                model.IdLanguages = new SelectList(sequenceQueryResult, "IdLanguage", "Language");
                model.lsContext = lsContext;
            }
            else
            {
                TempData["ResultError"] = "Error in adding new translation to resource! There is no more languages.";
                return RedirectToAction("ViewAllResources");
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTranslationStaticText(Resources model)
        {
            if (ModelState.IsValid)
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
            else
                TempData["ResultError"] = "Error in adding new translation!";
            return RedirectToAction("AddTranslationResource");
        }

        [HttpGet]
        public ActionResult EditStaticText()
        {
            return View();
        }


        #endregion
    }
}