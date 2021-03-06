﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    public class Resources
    {
        public int IdContext { get; set; }

        public int IdResource { get; set; }

        public string Language { get; set; }

        public SelectList IdLanguages { get; set; }

        public List<string> lsContext = new List<string>();

        [Required]
        public int IdLanguage { get; set; }

        [Required]
        public string Context { get; set; }
    }


    public class StaticResources
    {
        public int idStaticText { get; set; }

        public int IdStaticResource { get; set; }

        public string Language { get; set; }

        public SelectList IdLanguages { get; set; }

        [Required]
        public string Description { get; set; }

        public List<string> lsDescription = new List<string>();

        [Required]
        public int IdLanguage { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }

    public class StaticDetails
    {
        public string Description { get; set; }
        public string StaticText { get; set; }
    }
}