using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCG_Manage.Models
{
    public class LayoutModels
    {

        public string Name { get; set; }

        public int CountAllRoles { get; set; }

        public int CountAllUsers { get; set; }

        public string UrlProfilePicture { get; set; }
    }

    public class HeaderModel
    {
        public string UrlProfilePicture { get; set; }

        public string MemberSince { get; set; }

        public string Name { get; set; }
    }
}