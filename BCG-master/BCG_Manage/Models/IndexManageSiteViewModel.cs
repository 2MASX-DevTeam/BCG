using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCG_Manage.Models
{
    public class IndexManageSiteViewModel
    {
        public int SettedDays { get; set; }

        public List<ListOfActiveLanguages> listLanguages = new List<ListOfActiveLanguages>();

        public int UniqueVisitorsCounter { get; set; }

        public int UserRegistrationCounter { get; set; }

        public int NewOrdersCounter { get; set; }
    }

    public class ListOfActiveLanguages
    {
        public int IdLanguage { get; set; }

        public string Language { get; set; }

        public string Culture { get; set; }

        public string Initials { get; set; }
    }

}