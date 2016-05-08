using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BCG_Portal.Models;
using BCG_DB.Entity.Store;

namespace BCG_Portal.Controllers
{
    public class HomeController : Controller
    {
        private StoreModels db = new StoreModels();

        //[RequireHttps]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Products 


        public ActionResult Products()
        {
            var model = new ProductsViewModels();

            var tblProducts = db.tblProducts.Include(i => i.tblDiscount).Include(i => i.tblCurrency);
            var tblQuantitys = db.tblQuantities.ToList();

            foreach (var row in tblProducts)
            {
                    model.LstProducts.Add(new Product
                    {
                        IdProduct = row.IdProduct,
                        Name = row.ProductName,
                        ImageUrl = row.ImageUrl,
                        Media = row.Media,
                        Price = PriceForProduct(row.IdProduct)
                    });
            }

            model.ListQuantitys = new SelectList(tblQuantitys, "IdQuantity", "Quantity");

            return View(model);
        }

        private string PriceForProduct(int idProduct)
        {
            int id = idProduct;
            var tbl = (from tblProduct in db.tblProducts
                       join tblCurrency in db.tblCurrencies on tblProduct.IdCurrency equals tblCurrency.IdCurrency
                       where tblProduct.IdProduct == id
                       select new { Price = tblProduct.Price, CurrencyCode = tblCurrency.CurrencyCode, CurrencyValue = tblCurrency.CurrencyValue, IdDiscount = tblProduct.IdDiscount }).FirstOrDefault();

            var price = Convert.ToDecimal(tbl.Price);
            if (tbl.IdDiscount != null)
            {
                var discount = db.tblDiscounts.Find(tbl.IdDiscount).DiscountAmount;
                price = price - price * discount / 100;
            }
            var result = String.Format("{0} {1}", price, tbl.CurrencyCode);
            return result;
        }

        #endregion

        public ActionResult TermsOfUseBG()
        {
            return View();
        }

        public ActionResult CoockieDeclarationBG()
        {
            return View();
        }

        public ActionResult ConfirmEmail()
        {
            return View();
        }

        

    }
}