using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCG_Manage.Areas.EmailTemplates.Models
{
    public class CouponsModels
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string DiscountKey { get; set; }

        public string DiscountAmount { get; set; }

        public string DiscountDuration { get; set; }
    }
}