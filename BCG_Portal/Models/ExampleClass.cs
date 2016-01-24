using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCG_Portal.Models
{
    // An example class to store your values
    public class ExampleClass
    {
            // This attributes allows your HTML Content to be sent up
            [AllowHtml]
            public string HtmlContent { get; set; } 

    }
}