namespace BCG_DB.Entity.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblShopperGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblShopperGroup()
        {
            tblShoppers = new HashSet<tblShopper>();
        }

        [Key]
        public int IdShopperGroup { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }

        [StringLength(500)]
        public string GroupDescription { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblShopper> tblShoppers { get; set; }
    }
}
