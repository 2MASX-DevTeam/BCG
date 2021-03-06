﻿using BCG_DB.Entity.Store;
using BCG_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Security;

namespace BCG_Portal.Controllers
{
    public class StoreController : Controller
    {
        private StoreModels db = new StoreModels();

        

        public ActionResult AddToCard(string idProduct, ProductsViewModels models)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return PartialView("~/Views/Shared/_UserCard.cshtml", GetModelFromBasket());
            }
            if (Request.IsAjaxRequest())
            {
               
                var user = db.tblShoppers.Where(b => b.UserName == User.Identity.Name).FirstOrDefault();
                var basket = new BCG_DB.Entity.Store.tblShopingCart()
                {
                    IdShopper = user.IdShopper,
                    IdProduct = Convert.ToInt32(idProduct),
                    IdQuantity = Convert.ToInt32(models.SelectedQuantity),
                    UserName = User.Identity.Name,
                    DateChanged = DateTime.Now,
                    DateCreated = DateTime.Now
                };

                db.tblShopingCarts.Add(basket);
                db.SaveChanges();
            }
            return PartialView("~/Views/Shared/_UserCard.cshtml", GetModelFromBasket());
        }

        public ActionResult GetBasket()
        {
            return PartialView("~/Views/Shared/_UserCard.cshtml", GetModelFromBasket());
        }

        private UserCard GetModelFromBasket()
        {
            var model = new UserCard();
            if (!User.Identity.IsAuthenticated)
            {
                return model;
            }

            var user = db.tblShoppers.Where(b => b.UserName == User.Identity.Name ).FirstOrDefault();
            if (user != null)
            {
                var tblCart = (from tblShopingCart in db.tblShopingCarts
                               join tblQuantity in db.tblQuantities on tblShopingCart.IdQuantity equals tblQuantity.IdQuantity
                               join tblProduct in db.tblProducts on tblShopingCart.IdProduct equals tblProduct.IdProduct
                               select new
                               {
                                   IdShopingCart = tblShopingCart.IdShopingCart,
                                   IdProduct = tblProduct.IdProduct,
                                   ProductName = tblProduct.ProductName,
                                   Quantity = tblQuantity.Quantity,
                               }).ToList();

                decimal quantity = 0;
                foreach (var item in tblCart)
                {
                    var price = PriceForProduct(item.IdProduct);
                    model.Products.Add(new Card()
                    {
                        IdShopingCard = item.IdShopingCart,
                        ProductName = item.ProductName,
                        Price = price,
                        Quantity = item.Quantity
                    });
                    quantity += item.Quantity / 100 * Convert.ToDecimal(price.Split(' ')[0]);
                }
                model.Total =  quantity + " EU";

            }
            return model;
        }

        private string PriceForProduct(int idProduct)
        {
            int id = idProduct;
            var tbl = (from tblProduct in db.tblProducts
                       join tblCurrency in db.tblCurrencies on tblProduct.IdCurrency equals tblCurrency.IdCurrency
                       where tblProduct.IdProduct == id
                       select new
                       {
                           Price = tblProduct.Price,
                           CurrencyCode = tblCurrency.CurrencyCode,
                           CurrencyValue = tblCurrency.CurrencyValue,
                           IdDiscount = tblProduct.IdDiscount
                       }).FirstOrDefault();

            var price = Convert.ToDecimal(tbl.Price);
            if (tbl.IdDiscount != null)
            {
                var discount = db.tblDiscounts.Find(tbl.IdDiscount).DiscountAmount;
                price = price - price * discount / 100;
            }

            var result = String.Format("{0} {1}", price, tbl.CurrencyCode);
            return result;
        }


        public ActionResult DeleteProductFromCart(int IdShopingCard)
        {
            var tblShoppingCard = db.tblShopingCarts.Find(IdShopingCard);
            db.tblShopingCarts.Remove(tblShoppingCard);
            db.SaveChanges();

            return PartialView("~/Views/Shared/_UserCard.cshtml", GetModelFromBasket());
        }
        // GET: Store
        public ActionResult CheckOut()
        {
            var model = new CheckoutViewModel();
            var currencies = db.tblCurrencies.ToList();
            decimal total = 0;

            model.Products = GetModelFromBasket().Products;
            model.ListCurrencies = new SelectList(currencies, "CurrencyCode", "CurrencyCode");

            foreach (var item in model.Products)
            {
                total += item.Quantity / 100 * Convert.ToDecimal(item.Price.Split(' ')[0]);
            }
            model.Total = total + " EU";

            return View(model);
        }
    }
}