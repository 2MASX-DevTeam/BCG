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
    
    public partial class tblOrderStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblOrderStatus()
        {
            this.tblOrders = new HashSet<tblOrder>();
        }
    
        public int IdOrderStatus { get; set; }
        public string OrderStatusName { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> DateChanged { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrder> tblOrders { get; set; }
    }
}