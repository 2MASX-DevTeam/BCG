namespace BCG_DB.Entity.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblCustomerReview
    {
        [Key]
        public int IdCustomerReview { get; set; }

        public int IdProduct { get; set; }

        public int IdShopper { get; set; }

        public int? Rating { get; set; }

        public bool? IsPublish { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public string ReviewContext { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }
        
        public virtual tblShopper tblShoppers { get; set; }
        
        public virtual tblProduct tblProducts { get; set; }
    }
}
