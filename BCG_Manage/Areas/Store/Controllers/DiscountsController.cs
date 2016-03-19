using BCG_Manage.Areas.EmailTemplates.Models;

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
    public class DiscountsController : BaseController
    {
        private StoreModels db = new StoreModels();

        #region Add

        [HttpGet]
        public ActionResult AddNewDiscount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewDiscount(tblDiscount Model, string Reservationtime)
        {
            DateTime? startDate, endDate;
            int discount = 0;
            string reservationtime = Reservationtime;

            if (ModelState.IsValid && !String.IsNullOrEmpty(reservationtime))
            {
                try
                {
                    startDate = Convert.ToDateTime(reservationtime.Split('-')[0].Trim());
                    endDate = Convert.ToDateTime(reservationtime.Split('-')[1].Trim());
                    discount = Model.DiscountAmount;

                    var tbl = new tblDiscount
                    {
                        DiscountAmount = discount,
                        StartDateOfDiscount = startDate,
                        EndDateOfDiscount = endDate,
                        UserName = User.Identity.Name,
                        DateChanged = DateTime.Now,
                        DateCreated = DateTime.Now
                    };

                    db.tblDiscounts.Add(tbl);
                    db.SaveChanges();

                    TempData["ResultSuccess"] = "Success in adding new discount!";

                    return RedirectToAction("ViewAllDiscounts");
                }
                catch (Exception ex)
                {
                    TempData["ResultError"] = "Error in adding new discount!";
                    SendExceptionToAdmin(ex.ToString());
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        #endregion

        #region ViewAll

        public ActionResult ViewAllDiscounts(string sortOrder, string currentFilter, int? page)
        {
            var model = new List<DiscountsViewModel>();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdDiscountSortParm = String.IsNullOrEmpty(sortOrder) ? "IdDiscount" : "";
            ViewBag.AmountSortParm = sortOrder == "DiscountAmount" ? "DiscountAmountDesc" : "DiscountAmount";

            ViewBag.CountProductsNameSortParm = sortOrder == "CountUsers" ? "CountUsersDesc" : "CountUsers";
            ViewBag.CountUsersNameSortParm = sortOrder == "CountProducts" ? "CountProductsDesc" : "CountProducts";

            ViewBag.StartDateSortParm = sortOrder == "StartDateOfDiscount" ? "StartDateOfDiscountDesc" : "StartDateOfDiscount";
            ViewBag.EndDateSortParm = sortOrder == "EndDateOfDiscount" ? "EndDateOfDiscountDesc" : "EndDateOfDiscount";


            page = 1;

            var tbl = db.tblDiscounts.Where(i => i.DiscountKey == null).AsQueryable();


            foreach (var item in tbl)
            {
                model.Add(new DiscountsViewModel
                {
                    IdDiscount = item.IdDiscount,
                    DiscountAmount = item.DiscountAmount,
                    CounterProducts = CountProducts(item.IdDiscount),
                    CounterUsers = CountUsers(item.IdDiscount),
                    StartDate = item.StartDateOfDiscount,
                    EndDate = item.EndDateOfDiscount
                });
            }
            switch (sortOrder)
            {
                case "IdDiscount":
                    model = (List<DiscountsViewModel>)model.OrderByDescending(s => s.IdDiscount).ToList(); break;
                case "DiscountAmount":
                    model = (List<DiscountsViewModel>)model.OrderBy(s => s.DiscountAmount).ToList(); break;
                case "DiscountAmountDesc":
                    model = (List<DiscountsViewModel>)model.OrderByDescending(s => s.DiscountAmount).ToList(); break;
                case "CountProducts":
                    model = (List<DiscountsViewModel>)model.OrderBy(s => s.CounterProducts).ToList(); break;
                case "CountProductsDesc":
                    model = (List<DiscountsViewModel>)model.OrderByDescending(s => s.CounterProducts).ToList(); break;
                case "CountUsers":
                    model = (List<DiscountsViewModel>)model.OrderBy(s => s.CounterUsers).ToList(); break;
                case "CountUsersDesc":
                    model = (List<DiscountsViewModel>)model.OrderByDescending(s => s.CounterUsers).ToList(); break;
                case "StartDateOfDiscount":
                    model = (List<DiscountsViewModel>)model.OrderBy(s => s.StartDate).ToList(); break;
                case "StartDateOfDiscountDesc":
                    model = (List<DiscountsViewModel>)model.OrderByDescending(s => s.StartDate).ToList(); break;
                case "EndDateOfDiscount":
                    model = (List<DiscountsViewModel>)model.OrderBy(s => s.EndDate).ToList(); break;
                case "EndDateOfDiscountDesc":
                    model = (List<DiscountsViewModel>)model.OrderByDescending(s => s.EndDate).ToList(); break;


                default:
                    model = (List<DiscountsViewModel>)model.OrderBy(s => s.IdDiscount).ToList();
                    break;
            }
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = (page ?? 1);


            return View(model.ToPagedList(pageNumber, pageSize));

        }

        private int CountUsers(int idDiscount)
        {
            var id = idDiscount;
            var counter = db.tblShoppers.Count(i => i.IdDiscount == id);
            return counter;
        }

        private int CountProducts(int idDiscount)
        {
            var id = idDiscount;
            var counter = db.tblProducts.Where(i => i.IdDiscount == id).Count();
            return counter;
        }

        #endregion


        #region Apply to users

        public ActionResult ApplyDiscountToUsers(int? id)
        {
            id = 0;
            int idDiscount = (id ?? 0);
            var tbl = db.tblDiscounts.ToList().Select(s => new
            {
                IdDiscount = s.IdDiscount,
                DiscountKey = s.DiscountKey,
                DiscountPeriod = String.Format("Amount: {0}% - From Date -- {1}   To Date -- {2} ", s.DiscountAmount, s.StartDateOfDiscount, s.EndDateOfDiscount)
            }).Where(x => x.DiscountKey != null).OrderBy(s => s.IdDiscount);

            var model = new ApplyToUsersViewModel
            {
                SelectedDiscount = idDiscount.ToString(),
                ListDiscounts = new SelectList(tbl, "IdDiscount", "DiscountPeriod")
            };


            return View(model);
        }


        private int CountOrdersForUser(int idShopper)
        {
            var id = idShopper;
            var counter = (from tblCompleteOrder in db.tblCompleteOrders
                           where tblCompleteOrder.IdShopper == id
                           select tblCompleteOrder.IdShopper
                             ).Count();
            return counter;
        }

        public ActionResult ShoppersForDIscount(int? id)
        {
            //id = 0;
            int idDiscount = (id ?? 0);

            var model = new List<ShopersForDiscountsModel>();

            var tbl = db.tblShoppers.Include(i => i.tblDiscount);
            foreach (var item in tbl)
            {

                model.Add(new ShopersForDiscountsModel
                {
                    IdDiscount = idDiscount,
                    IdShopper = item.IdShopper,
                    FullName = String.Format("{0} {1} {2}", item.FirstName, item.SecondName, item.LastName),
                    OrdersCount = CountOrdersForUser(item.IdShopper),
                    DiscountAmount = (item.tblDiscount == null) ? 0 : item.tblDiscount.DiscountAmount,
                    PeriodDiscount = (item.tblDiscount != null) ? item.tblDiscount.StartDateOfDiscount + " - " + item.tblDiscount.EndDateOfDiscount : "",
                    IsMessageSended = item.IsMessageForDiscountSended.GetValueOrDefault()
                });
            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = 1;

            return PartialView("~/Areas/Store/Views/Discounts/_ShoppersPartial.cshtml", model.ToPagedList(pageNumber, pageSize));
        }

        public JsonResult ChangeUserDiscount(int id, int disc, bool check)
        {
            try
            {
                var icheck = check;
                var idShopper = id;
                var idDiscount = disc;

                if (idDiscount == 0)
                {
                    var firstOrDefault = db.tblDiscounts.FirstOrDefault();
                    if (firstOrDefault != null) idDiscount = firstOrDefault.IdDiscount;
                }

                var tbl = db.tblShoppers.Find(idShopper);
                var discountAmount = 0;
                var period = "";

                if (icheck)
                {
                    tbl.IdDiscount = idDiscount;
                    discountAmount = db.tblDiscounts.Find(idDiscount).DiscountAmount;
                    period = String.Format("{0} - {1}", db.tblDiscounts.Find(idDiscount).StartDateOfDiscount,
                        db.tblDiscounts.Find(idDiscount).EndDateOfDiscount);
                }
                else
                {
                    tbl.IdDiscount = null;
                    tbl.IsMessageForDiscountSended = false;
                }

                db.SaveChanges();
                var data = new List<string>();
                data.Add(discountAmount.ToString());
                data.Add(period);

                var newDiscount = discountAmount;

                return Json(data);
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                return Json("ERROR!");
            }

        }


        public JsonResult SendCouponToUser(int IdShopper)
        {
            int idShopper = IdShopper;

            if (Request.IsAjaxRequest())
            {

                try
                {
                    var strDiscountPeriod = "";
                    var name = "";

                    var tbl = db.tblShoppers.Find(idShopper);
                    var tblDisc = db.tblDiscounts.Find(tbl.IdDiscount);
                    strDiscountPeriod = String.Format("From: {0} To: {1}", tblDisc.StartDateOfDiscount,
                tblDisc.EndDateOfDiscount);

                    name = String.Format("{0} {1}", tbl.FirstName, tbl.LastName);

                    var model = new CouponsModels
                    {
                        Name = name,
                        Email = tbl.Email,
                        DiscountDuration = strDiscountPeriod,
                        DiscountKey = tblDisc.DiscountKey,
                        DiscountAmount = tblDisc.DiscountAmount + " %"
                    };


                    var emailBody = RenderViewToString("CouponsEmail", model);
                    SendEmail(tbl.Email, "New coupon", emailBody);

                    tbl.IsMessageForDiscountSended = true;
                    db.SaveChanges();
                    return Json(tbl.Email.ToString());

                }
                catch (Exception ex)
                {
                    SendExceptionToAdmin(ex.ToString());
                    return Json("Error in update");
                }
            }
            return Json("Error!");
        }

        #endregion

        #region Apply to products

        public ActionResult ApplyDiscountToProducts(int? id)
        {
            id = 0;
            int idDiscount = (id ?? 0);
            var tbl = db.tblDiscounts.ToList().Select(s => new
            {
                IdDiscount = s.IdDiscount,
                DiscountKey = s.DiscountKey,
                DiscountPeriod = String.Format("Amount: {0}% - From Date -- {1}   To Date -- {2} ", s.DiscountAmount, s.StartDateOfDiscount, s.EndDateOfDiscount)
            }).Where(x => x.DiscountKey == null).OrderBy(s => s.IdDiscount);

            var model = new ApplyToUsersViewModel
            {
                SelectedDiscount = idDiscount.ToString(),
                ListDiscounts = new SelectList(tbl, "IdDiscount", "DiscountPeriod")
            };


            return View(model);
        }

        public ActionResult ProductsForDIscount(int? id)
        {
            //id = 0;
            int idDiscount = (id ?? 0);

            var model = new List<ProductsForDiscountsModel>();

            var tbl = db.tblProducts.Include(i => i.tblDiscount).Include(i => i.tblCurrency);
            foreach (var item in tbl)
            {
                if (item.IdDiscount == null)
                {
                    model.Add(new ProductsForDiscountsModel
                    {
                        IdProduct = item.IdProduct,
                        Media = item.Media,
                        OrdersCount = CountOrdersForProducts(item.IdProduct),
                        ProductName = item.ProductName,
                        IsPublish = item.IsPublish,
                        Price = PriceForProduct(item.IdProduct)
                    });
                    continue;

                }
                model.Add(new ProductsForDiscountsModel
                {
                    IdDiscount = idDiscount,
                    DiscountAmount = item.tblDiscount.DiscountAmount,
                    IdProduct = item.IdProduct,
                    Media = item.Media,
                    OrdersCount = CountOrdersForProducts(item.IdProduct),
                    ProductName = item.ProductName,
                    IsPublish = item.IsPublish,
                    PeriodDiscount = (item.tblDiscount != null) ? item.tblDiscount.StartDateOfDiscount + " - " + item.tblDiscount.EndDateOfDiscount : "",
                    Price = PriceForProduct(item.IdProduct)
                });

            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = 1;

            return PartialView("~/Areas/Store/Views/Discounts/_ProductsPartial.cshtml", model.ToPagedList(pageNumber, pageSize));
        }

        private int CountOrdersForProducts(int idProduct)
        {
            var id = idProduct;
            var counter = (from tblOrder in db.tblOrders
                           where tblOrder.IdProduct == id
                           select tblOrder.IdOrder).Count();
            return counter;
        }

        public JsonResult ChangeProductDiscount(int id, int disc, bool check)
        {
            try
            {
                var icheck = check;
                var idProduct = id;
                var idDiscount = disc;

                if (idDiscount == 0)
                {
                    var firstOrDefault = db.tblDiscounts.Where(i => i.DiscountKey == null).FirstOrDefault();
                    if (firstOrDefault != null) idDiscount = firstOrDefault.IdDiscount;
                }

                var tbl = db.tblProducts.Find(idProduct);
                var discountAmount = 0;
                var period = "";
                var price = "";
                if (icheck)
                {
                    tbl.IdDiscount = idDiscount;
                    discountAmount = db.tblDiscounts.Find(idDiscount).DiscountAmount;
                    period = String.Format("{0} - {1}", db.tblDiscounts.Find(idDiscount).StartDateOfDiscount, db.tblDiscounts.Find(idDiscount).EndDateOfDiscount);

                }
                else
                    tbl.IdDiscount = null;


                db.SaveChanges();
                var data = new List<string>();
                data.Add(discountAmount.ToString());
                data.Add(period);

                price = PriceForProduct(idProduct);

                data.Add(price);
                var newDiscount = discountAmount;

                return Json(data);
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                return Json("ERROR!");
            }
        }

        #endregion

        public ActionResult DeleteDiscount(int id)
        {
            var idDiscount = id;
            using (var scope = db.Database.BeginTransaction())
            {
                try
                {

                    var tblProd = db.tblProducts.Where(i => i.IdDiscount == idDiscount);
                    foreach (var item in tblProd)
                    {
                        item.IdDiscount = null;
                    }

                    var tbl = db.tblDiscounts.Find(idDiscount);
                    db.tblDiscounts.Remove(tbl);
                    db.SaveChanges();

                    scope.Commit();

                    TempData["ResultSuccess"] = "Success in deleting discount!";
                    return RedirectToAction("ViewAllDiscounts");
                }
                catch
                (Exception ex)
                {

                    TempData["ResultError"] = "Error in deleting discount!";
                    scope.Rollback();
                    SendExceptionToAdmin(ex.ToString());
                    return RedirectToAction("ViewAllDiscounts");
                }
            }
        }
    }
}