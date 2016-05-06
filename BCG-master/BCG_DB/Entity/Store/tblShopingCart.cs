namespace BCG_DB.Entity.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblShopingCart")]
    public partial class tblShopingCart
    {
        [Key]
        public int IdShopingCart { get; set; }

        public int IdShopper { get; set; }

        public int IdProduct { get; set; }

        public int IdQuantity { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual tblProduct tblProduct { get; set; }

        public virtual tblQuantity tblQuantity { get; set; }

        public virtual tblShopper tblShopper { get; set; }
    }
}
