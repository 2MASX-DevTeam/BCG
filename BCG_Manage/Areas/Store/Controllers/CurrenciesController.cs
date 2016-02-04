
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
    using System.Net;
    using Newtonsoft.Json;

    [Authorize]
    public class CurrenciesController : BaseController
    {
        private bool isAllGood = false;

        [HttpGet]
        public ActionResult AddNewCurrency()
        {
            //  TempData["ResultInfo"] = @"Example:  Currency Name = Bulgarian Lev , Currency Code = BGN , Currency Value = 1.5";
            TempData["ResultAlert"] = "Each new currency will be compared  to Euro";


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewCurrency(tblCurrency model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var symbol = model.CurrencyCode;

                    var currencyData = GetCurrencieExchangeRate(symbol);

                    if (isAllGood)
                    {
                        //TODO: Add insert in db

                        TempData["ResultAlert"] = currencyData.rates[symbol];
                        return RedirectToAction("AddNewCurrency");
                    }
                }
                catch (Exception ex)
                {
                    SendExceptionToAdmin(ex.ToString());
                }

            }

            TempData["ResultError"] = "Something went wrong! ";
            return View();
        }


        public dynamic GetCurrencieExchangeRate(string CurrencieSymbol)
        {
            try
            {
                string url = "http://api.fixer.io/latest?symbols=EUR," + CurrencieSymbol;
                WebClient client = new WebClient();
                string jsonstring = client.DownloadString(url);
                dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);

                isAllGood = true;
                return dynObj;
            }
            catch (Exception ex)
            {
                // SendExceptionToAdmin(ex.ToString());
                return null;
            }

        }
    }
}