//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BCG_Portal_Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCustomerReview
    {
        public int IdCustomerReview { get; set; }
        public int IdProduct { get; set; }
        public int IdShopper { get; set; }
        public Nullable<int> Rating { get; set; }
        public string ReviewContext { get; set; }
        public Nullable<bool> IsPublish { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> DateChanged { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual tblProduct tblProduct { get; set; }
        public virtual tblShopper tblShopper { get; set; }
    }
}
