namespace BCG_DB.Entity.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblShopper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblShopper()
        {
            tblCustomerReviews = new HashSet<tblCustomerReview>();
            tblOrders = new HashSet<tblOrder1>();
            tblShopingCarts = new HashSet<tblShopingCart>();
        }

        [Key]
        public int IdShopper { get; set; }

        public int? IdShopperGroup { get; set; }

        public int? IdDiscount { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string SecondName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public int? MobilePhone { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerReview> tblCustomerReviews { get; set; }

        public virtual tblDiscount tblDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrder1> tblOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblShopingCart> tblShopingCarts { get; set; }

        public virtual tblShopperGroup tblShopperGroup { get; set; }
    }
}
