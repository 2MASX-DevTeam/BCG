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
    
    public partial class tblStaticResource
    {
        public int IdStatic { get; set; }
        public int IdLanguage { get; set; }
        public int IdStaticText { get; set; }
        public string Description { get; set; }
        public string StaticName { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> DateChanged { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual tblLanguage tblLanguage { get; set; }
        public virtual tblStaticText tblStaticText { get; set; }
    }
}
