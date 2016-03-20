using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BCG_Portal.Models.BCGDB_Entities;

namespace BCG_Portal.Controllers
{
    public class ShoppersController : Controller
    {
        private bcvitbg_Entity db = new bcvitbg_Entity();

       public ActionResult RegisterShopper()
       {
           return View();
        }

        // GET: Shoppers
        public ActionResult Index()
        {
            var tblShoppers = db.tblShoppers.Include(t => t.tblDiscount).Include(t => t.tblShopperGroup);
            return View(tblShoppers.ToList());
        }

        // GET: Shoppers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblShopper tblShopper = db.tblShoppers.Find(id);
            if (tblShopper == null)
            {
                return HttpNotFound();
            }
            return View(tblShopper);
        }

        // GET: Shoppers/Create
        public ActionResult Create()
        {
            ViewBag.IdDiscount = new SelectList(db.tblDiscounts, "IdDiscount", "DiscountKey");
            ViewBag.IdShopperGroup = new SelectList(db.tblShopperGroups, "IdShopperGroup", "GroupName");
            return View();
        }

        // POST: Shoppers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdShopper,IdShopperGroup,IdDiscount,UserName,Email,EmailConfirmed,Password,FirstName,SecondName,LastName,MobilePhone,BirthDate,Country,CompanyName,IsMessageForDiscountSended,DateCreated")] tblShopper tblShopper)
        {
            if (ModelState.IsValid)
            {
                db.tblShoppers.Add(tblShopper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDiscount = new SelectList(db.tblDiscounts, "IdDiscount", "DiscountKey", tblShopper.IdDiscount);
            ViewBag.IdShopperGroup = new SelectList(db.tblShopperGroups, "IdShopperGroup", "GroupName", tblShopper.IdShopperGroup);
            return View(tblShopper);
        }

        // GET: Shoppers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblShopper tblShopper = db.tblShoppers.Find(id);
            if (tblShopper == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDiscount = new SelectList(db.tblDiscounts, "IdDiscount", "DiscountKey", tblShopper.IdDiscount);
            ViewBag.IdShopperGroup = new SelectList(db.tblShopperGroups, "IdShopperGroup", "GroupName", tblShopper.IdShopperGroup);
            return View(tblShopper);
        }

        // POST: Shoppers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdShopper,IdShopperGroup,IdDiscount,UserName,Email,EmailConfirmed,Password,FirstName,SecondName,LastName,MobilePhone,BirthDate,Country,CompanyName,IsMessageForDiscountSended,DateCreated")] tblShopper tblShopper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblShopper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDiscount = new SelectList(db.tblDiscounts, "IdDiscount", "DiscountKey", tblShopper.IdDiscount);
            ViewBag.IdShopperGroup = new SelectList(db.tblShopperGroups, "IdShopperGroup", "GroupName", tblShopper.IdShopperGroup);
            return View(tblShopper);
        }

        // GET: Shoppers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblShopper tblShopper = db.tblShoppers.Find(id);
            if (tblShopper == null)
            {
                return HttpNotFound();
            }
            return View(tblShopper);
        }

        // POST: Shoppers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblShopper tblShopper = db.tblShoppers.Find(id);
            db.tblShoppers.Remove(tblShopper);
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
