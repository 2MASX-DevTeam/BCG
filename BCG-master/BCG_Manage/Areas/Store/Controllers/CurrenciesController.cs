
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
        private StoreModels db = new StoreModels();
        private bool isAllGood = false;

        [HttpGet]
        public ActionResult AddNewCurrency()
        {
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
                    var symbol = model.CurrencyCode.ToUpper();

                    if (db.tblCurrencies.Any(o => o.CurrencyCode == symbol))
                    {
                        TempData["ResultError"] = "There is already currency with this code in database!";
                        return RedirectToAction("AddNewCurrency");
                    }

                    var currencyData = GetCurrencieExchangeRate(symbol);

                    if (isAllGood)
                    {

                        var value = currencyData.rates[symbol];

                        var tbl = new tblCurrency
                        {
                            CurrencyCode = model.CurrencyCode,
                            CurrencyName = model.CurrencyName,
                            CurrencyValue = Convert.ToDecimal(value),
                            UserName = User.Identity.Name,
                            DateChanged = DateTime.Now,
                            DateCreated = DateTime.Now
                        };

                        db.tblCurrencies.Add(tbl);
                        db.SaveChanges();

                        return RedirectToAction("ViewAllCurrencies");
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

    
        public ActionResult ViewAllCurrencies(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdCurrencySortParm = String.IsNullOrEmpty(sortOrder) ? "IdCurrency" : "";
            ViewBag.CurrencyNameSortParm = sortOrder == "CurrencyName" ? "CurrencyNameDesc" : "CurrencyName";
            ViewBag.CurrencyCodeSortParm = sortOrder == "CurrencyCode" ? "CurrencyCodeDesc" : "CurrencyCode";
            ViewBag.CurrencyValueSortParm = sortOrder == "CurrencyValue" ? "CurrencyValueDesc" : "CurrencyValue";
            ViewBag.DateModifySortParm = sortOrder == "DateChanged" ? "DateChangedDesc" : "DateChanged";
            ViewBag.DateCreatedSortParm = sortOrder == "DateCreated" ? "DateCreatedDesc" : "DateCreated";

            if (searchString != null)
                page = 1;

            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var tbl = db.tblCurrencies.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                tbl = tbl.Where(s => s.CurrencyName.Contains(searchString));

            switch (sortOrder)
            {
                case "IdCurrency":
                    tbl = tbl.OrderByDescending(s => s.IdCurrency); break;
                case "CurrencyName":
                    tbl = tbl.OrderBy(s => s.CurrencyName); break;
                case "CurrencyNameDesc":
                    tbl = tbl.OrderByDescending(s => s.CurrencyName); break;
                case "CurrencyCode":
                    tbl = tbl.OrderBy(s => s.CurrencyCode); break;
                case "CurrencyCodeDesc":
                    tbl = tbl.OrderByDescending(s => s.CurrencyCode); break;
                case "CurrencyValue":
                    tbl = tbl.OrderBy(s => s.CurrencyValue); break;
                case "CurrencyValueDesc":
                    tbl = tbl.OrderByDescending(s => s.CurrencyValue); break;
                case "DateChanged":
                    tbl = tbl.OrderBy(s => s.DateChanged); break;
                case "DateChangedDesc":
                    tbl = tbl.OrderByDescending(s => s.DateChanged); break;
                case "DateCreated":
                    tbl = tbl.OrderBy(s => s.DateCreated); break;
                case "DateCreatedDesc":
                    tbl = tbl.OrderByDescending(s => s.DateCreated); break;


                default:
                    tbl = tbl.OrderBy(s => s.IdCurrency);
                    break;
            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = (page ?? 1);


            return View(tbl.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DeleteCurrency(int id)
        {
            try
            {

                var tbl = db.tblCurrencies.Find(id);
                db.tblCurrencies.Remove(tbl);
                db.SaveChanges();
                TempData["ResultSuccess"] = "Success in deleting Currency!";
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in deleting Currency!";

            }


            return RedirectToAction("ViewAllCurrencies");
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