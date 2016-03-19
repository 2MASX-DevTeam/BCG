namespace BCG_Manage.Areas.Store.Controllers
{
    using BCG_DB.Entity.Store;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PagedList;
    using System.IO;
    using System.Drawing;
    using System.Data.Entity;
    using Models;
    using System.Configuration;
    using System.Data.SqlClient;
    using BCG_Manage.Controllers;

    [Authorize]
    public class CouponsController : BaseController
    {
        private StoreModels db = new StoreModels();

        [HttpGet]
        public ActionResult AddNewCoupon()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewCoupon(tblDiscount model, string Reservationtime)
        {
            DateTime? startDate, endDate;
            int discount = 0;
            string reservationtime = Reservationtime;
            string code = GenericSymbols();

            if (ModelState.IsValid && !String.IsNullOrEmpty(reservationtime))
            {
                try
                {
                    startDate = Convert.ToDateTime(reservationtime.Split('-')[0].Trim());
                    endDate = Convert.ToDateTime(reservationtime.Split('-')[1].Trim());
                    discount = model.DiscountAmount;

                    var tbl = new tblDiscount
                    {
                        DiscountKey = code,
                        DiscountAmount = discount,
                        StartDateOfDiscount = startDate,
                        EndDateOfDiscount = endDate,
                        UserName = User.Identity.Name,
                        DateChanged = DateTime.Now,
                        DateCreated = DateTime.Now
                    };

                    db.tblDiscounts.Add(tbl);
                    db.SaveChanges();

                    TempData["ResultSuccess"] = "Success in adding new coupon!";

                    return RedirectToAction("ViewAllCoupons");
                }
                catch (Exception ex)
                {
                    TempData["ResultError"] = "Error in adding new coupon!";
                    SendExceptionToAdmin(ex.ToString());
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        public ActionResult ViewAllCoupons(string sortOrder, string currentFilter, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdCouponSortParm = String.IsNullOrEmpty(sortOrder) ? "IdDiscount" : "";
            ViewBag.AmountSortParm = sortOrder == "DiscountAmount" ? "DiscountAmountDesc" : "DiscountAmount";
            ViewBag.KeyNameSortParm = sortOrder == "DiscountKey" ? "DiscountKeyDesc" : "DiscountKey";
            ViewBag.StartDateSortParm = sortOrder == "StartDateOfDiscount" ? "StartDateOfDiscountDesc" : "StartDateOfDiscount";
            ViewBag.EndDateSortParm = sortOrder == "EndDateOfDiscount" ? "EndDateOfDiscountDesc" : "EndDateOfDiscount";

            
            page = 1;

            var tbl = db.tblDiscounts.Where(i => i.DiscountKey != null).AsQueryable();

            switch (sortOrder)
            {
                case "IdDiscount":
                    tbl = tbl.OrderByDescending(s => s.IdDiscount); break;
                case "DiscountAmount":
                    tbl = tbl.OrderBy(s => s.DiscountAmount); break;
                case "DiscountAmountDesc":
                    tbl = tbl.OrderByDescending(s => s.DiscountAmount); break;
                case "DiscountKey":
                    tbl = tbl.OrderBy(s => s.DiscountKey); break;
                case "DiscountKeyDesc":
                    tbl = tbl.OrderByDescending(s => s.DiscountKey); break;
                case "StartDateOfDiscount":
                    tbl = tbl.OrderBy(s => s.StartDateOfDiscount); break;
                case "StartDateOfDiscountDesc":
                    tbl = tbl.OrderByDescending(s => s.StartDateOfDiscount); break;
                case "EndDateOfDiscount":
                    tbl = tbl.OrderBy(s => s.EndDateOfDiscount); break;
                case "EndDateOfDiscountDesc":
                    tbl = tbl.OrderByDescending(s => s.EndDateOfDiscount); break;


                default:
                    tbl = tbl.OrderBy(s => s.IdDiscount);
                    break;
            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = (page ?? 1);


            return View(tbl.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult DeleteDiscount(int id)
        {
            try
            {

                var tbl = db.tblDiscounts.Find(id);
                db.tblDiscounts.Remove(tbl);
                db.SaveChanges();
                TempData["ResultSuccess"] = "Success in deleting coupon!";
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in deleting!";

            }


            return RedirectToAction("ViewAllCoupons");
        }
    }
}