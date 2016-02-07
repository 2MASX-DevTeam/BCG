using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCG_Manage.Areas.Store.Models
{
    public class CustomerReviewsViewModel
    {
        public int IdReview { get; set; }

        public string ProductName { get; set; }

        public DateTime? DatePublished { get; set; }

        public string ReviewContext { get; set; }

        public int? Rating { get; set; }

        public bool? IsPublish { get; set; }

        public string PublisherName { get; set; }
    }
}