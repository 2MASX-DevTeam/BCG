namespace BCG_DB.Entity.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblCompleteOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCompleteOrder()
        {
            tblOrders = new HashSet<tblOrder>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdOrders { get; set; }

        public int IdShopper { get; set; }

        public bool? IsPaid { get; set; }

        public DateTime? DatePayment { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrder> tblOrders { get; set; }

        public virtual tblShopper tblShopper { get; set; }
    }
}
