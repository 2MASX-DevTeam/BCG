namespace BCG_DB.Entity.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblOrder")]
    public partial class tblOrder
    {
        [Key]
        public int IdOrder { get; set; }

        public int IdOrders { get; set; }

        public int IdProduct { get; set; }

        public int IdOrderStatus { get; set; }

        public int IdQuantity { get; set; }

        public DateTime OrderDate { get; set; }

        public int QuantityLeft { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual tblCompleteOrder tblCompleteOrder { get; set; }

        public virtual tblProduct tblProduct { get; set; }

        public virtual tblQuantity tblQuantity { get; set; }

        public virtual tblOrderStatus tblOrderStatus { get; set; }
    }
}
