using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BCG_Manage.Models
{
    public class LanguagesInModels
    {
        public int IdLanguage { get; set; }
        
        [Required]
        public string Language { get; set; }

        [Required]
        public string Initials { get; set; }

        [Required]
        public string Culture { get; set; }


        public HttpPostedFileBase Picture { get; set; }

        [Required]
        public bool IsActive { get; set; }
        
        
    }
}