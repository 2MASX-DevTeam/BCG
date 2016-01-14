using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BCG_DB.Entity.MultiLanguageEntity;

namespace BCG_Manage.Controllers
{
    public class LanguagesController : Controller
    {
        private MultiLanguageModel db = new MultiLanguageModel();

        // GET: Languages
        public ActionResult Index()
        {
            return View(db.tblLanguages.ToList());
        }

        // GET: Languages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLanguages tblLanguages = db.tblLanguages.Find(id);
            if (tblLanguages == null)
            {
                return HttpNotFound();
            }
            return View(tblLanguages);
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLanguage,Language,Initials,Culture,Picture,IsActive,UserName,Datechanged,DateCreated")] tblLanguages tblLanguages)
        {
            if (ModelState.IsValid)
            {
                db.tblLanguages.Add(tblLanguages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblLanguages);
        }

        // GET: Languages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLanguages tblLanguages = db.tblLanguages.Find(id);
            if (tblLanguages == null)
            {
                return HttpNotFound();
            }
            return View(tblLanguages);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLanguage,Language,Initials,Culture,Picture,IsActive,UserName,Datechanged,DateCreated")] tblLanguages tblLanguages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLanguages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblLanguages);
        }

        // GET: Languages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLanguages tblLanguages = db.tblLanguages.Find(id);
            if (tblLanguages == null)
            {
                return HttpNotFound();
            }
            return View(tblLanguages);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLanguages tblLanguages = db.tblLanguages.Find(id);
            db.tblLanguages.Remove(tblLanguages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
