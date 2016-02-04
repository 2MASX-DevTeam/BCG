
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
    public class OrdersController : BaseController
    {
        private StoreModels db = new StoreModels();

        // GET: Store/Orders
        public ActionResult ViewAll(string sortOrder, string currentFilter, string searchString, int? page)
        {

            var model = new List<Store.Models.OrdersViewModel>();

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
            try
            {
                var allOrders = (from tblCompleteOrders in db.tblCompleteOrders
                                 join tblOrders in db.tblOrders on tblCompleteOrders.IdOrders equals tblOrders.IdOrders
                                 join tblProduct in db.tblProducts on tblOrders.IdProduct equals tblProduct.IdProduct
                                 join tblOrderStatus in db.tblOrderStatuses on tblOrders.IdOrderStatus equals tblOrderStatus.IdOrderStatus
                                 join tblQuantity in db.tblQuantities on tblOrders.IdQuantity equals tblQuantity.IdQuantity
                                 join tblShopper in db.tblShoppers on tblCompleteOrders.IdShopper equals tblShopper.IdShopper
                                 join tblShopperDiscount in db.tblDiscounts on tblShopper.IdDiscount equals tblShopperDiscount.IdDiscount
                                 join tblDiscount in db.tblDiscounts on tblProduct.IdDiscount equals tblDiscount.IdDiscount
                                 join tblCurrency in db.tblCurrencies on tblProduct.IdCurrency equals tblCurrency.IdCurrency
                                 where tblShopperDiscount.StartDateOfDiscount > DateTime.Now && tblShopperDiscount.EndDateOfDiscount < DateTime.Now
                                 where tblDiscount.StartDateOfDiscount > DateTime.Now && tblDiscount.EndDateOfDiscount < DateTime.Now
                                 orderby tblOrders.OrderDate
                                 select new
                                 {
                                     IdOrder = tblCompleteOrders.IdOrders,
                                     UserFullName = tblShopper.FirstName + " " + tblShopper.SecondName + " " + tblShopper.LastName,
                                     Quantity = tblQuantity.Quantity,
                                     QuantityLeft = tblOrders.QuantityLeft,
                                     Product = tblProduct.ProductName,
                                     OrderDate = tblOrders.OrderDate,
                                     PaymentDate = tblCompleteOrders.DatePayment,
                                     Status = tblOrderStatus.OrderStatusName,
                                     Price = tblProduct.Price,
                                     Currencie = tblCurrency.CurrencyValue,
                                     Discount = (tblShopperDiscount.DiscountAmount > tblDiscount.DiscountAmount) ? tblShopperDiscount.DiscountAmount : tblDiscount.DiscountAmount
                                 }
                           ).ToList();

                foreach (var item in allOrders)
                {
                    model.Add(new OrdersViewModel
                    {
                        IdOrder = item.IdOrder,
                        UserFullName = item.UserFullName,
                        Quantity = item.Quantity,
                        QuantityLeft = item.QuantityLeft,
                        Product = item.Product,
                        OrderDate = item.OrderDate,
                        PaymentDate = item.PaymentDate,
                        Status = item.Status,
                        Price = item.Price,
                        Currencie = item.Currencie,
                        Discount = item.Discount
                    });
                }

            }
            catch (Exception ex)
            {
                TempData["ResultError"] = "Error in loading orders!";
                SendExceptionToAdmin(ex.ToString());
            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult NotConfirmed()
        {
            return View();
        }
    }
}