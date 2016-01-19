using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCG_Manage.Areas.EmailTemplates.Models
{
    public class EmailModels
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Link { get; set; }
    }
}