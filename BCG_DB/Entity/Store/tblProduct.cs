namespace BCG_DB.Entity.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProduct()
        {
            tblCustomerReviews = new HashSet<tblCustomerReview>();
            tblOrders = new HashSet<tblOrder>();
            tblShopingCarts = new HashSet<tblShopingCart>();
        }

        [Key]
        public int IdProduct { get; set; }

        public int? IdDiscount { get; set; }

        public int IdCurrency { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        public string Media { get; set; }

        [StringLength(50)]
        public string Price { get; set; }

        public bool? IsPublish { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual tblCurrency tblCurrency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerReview> tblCustomerReviews { get; set; }

        public virtual tblDiscount tblDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrder> tblOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblShopingCart> tblShopingCarts { get; set; }
    }
}
