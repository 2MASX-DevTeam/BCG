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

    public class CustomerReviewsController : BaseController
    {
        private StoreModels db = new StoreModels();

        public ActionResult ViewAllReviews(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var model = new List<CustomerReviewsViewModel>();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdReviewSortParm = String.IsNullOrEmpty(sortOrder) ? "IdCustomerReview" : "";
            ViewBag.ProductNameSortParm = sortOrder == "ProductName" ? "ProductNameDesc" : "ProductName";
            ViewBag.PublisherNameSortParm = sortOrder == "PublisherName" ? "PublisherNameDesc" : "PublisherName";
            ViewBag.DateSortParm = sortOrder == "DateCreated" ? "DateCreatedDesc" : "DateCreated";
            ViewBag.TextSortParm = sortOrder == "ReviewContext" ? "ReviewContextDesc" : "ReviewContext";
            ViewBag.RatingSortParm = sortOrder == "Rating" ? "RatingDesc" : "Rating";
            ViewBag.PublishSortParm = sortOrder == "IsPublish" ? "IsPublishDesc" : "IsPublish";

            if (searchString != null)
                page = 1;

            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var tbl = db.tblCustomerReviews.Include(t => t.tblProducts).Include(e => e.tblShoppers);

            if (!String.IsNullOrEmpty(searchString))
                tbl = tbl.Where(s => s.ReviewContext.Contains(searchString));

            switch (sortOrder)
            {
                case "IdCustomerReview":
                    tbl = tbl.OrderByDescending(s => s.IdCustomerReview); break;
                case "ProductName":
                    tbl = tbl.OrderBy(s => s.tblProducts.ProductName); break;
                case "ProductNameDesc":
                    tbl = tbl.OrderByDescending(s => s.tblProducts.ProductName); break;
                case "PublisherName":
                    tbl = tbl.OrderBy(s => s.tblShoppers.FirstName); break;
                case "PublisherNameDesc":
                    tbl = tbl.OrderByDescending(s => s.tblShoppers.FirstName); break;
                case "DateCreated":
                    tbl = tbl.OrderBy(s => s.DateCreated); break;
                case "DateCreatedDesc":
                    tbl = tbl.OrderByDescending(s => s.DateCreated); break;
                case "ReviewContext":
                    tbl = tbl.OrderBy(s => s.ReviewContext); break;
                case "ReviewContextDesc":
                    tbl = tbl.OrderByDescending(s => s.ReviewContext); break;
                case "Rating":
                    tbl = tbl.OrderBy(s => s.Rating); break;
                case "RatingDesc":
                    tbl = tbl.OrderByDescending(s => s.Rating); break;
                case "IsPublish":
                    tbl = tbl.OrderBy(s => s.IsPublish); break;
                case "IsPublishDesc":
                    tbl = tbl.OrderByDescending(s => s.IsPublish); break;


                default:
                    tbl = tbl.OrderBy(s => s.IdCustomerReview);
                    break;
            }
            foreach (var row in tbl)
            {
                model.Add(new CustomerReviewsViewModel {
                    IdReview = row.IdCustomerReview,
                    ProductName = row.tblProducts.ProductName,
                    PublisherName = row.tblShoppers.FirstName + row.tblShoppers.LastName,
                    DatePublished = row.DateCreated,
                    ReviewContext = row.ReviewContext,
                    Rating = row.Rating,
                    IsPublish = row.IsPublish
                });
            }
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ItemsPerPage"]);
            int pageNumber = (page ?? 1);


            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DeleteReview(int id)
        {
            try
            {

                var tbl = db.tblCustomerReviews.Find(id);
                db.tblCustomerReviews.Remove(tbl);
                db.SaveChanges();
                TempData["ResultSuccess"] = "Success in deleting customer review!";
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
                TempData["ResultError"] = "Error in deleting!";

            }


            return RedirectToAction("ViewAllReviews");
        }
    }
}