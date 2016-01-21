namespace BCG_DB.Entity.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblIPLoginAttempts
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdIPAdress { get; set; }

      
        [StringLength(50)]
        public string IPAdress { get; set; }

        [StringLength(500)]
        public string UserAgend { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }
    }
}
