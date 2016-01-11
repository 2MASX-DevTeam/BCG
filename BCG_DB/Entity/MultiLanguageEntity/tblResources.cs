namespace BCG_DB.Entity.MultiLanguageEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblResources
    {
        [Key]
        public int IdResource { get; set; }

        public int IdLanguage { get; set; }

        public int IdContext { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        [StringLength(50)]
        public string DateCreated { get; set; }

        public virtual tblContext tblContext { get; set; }

        public virtual tblLanguages tblLanguages { get; set; }
    }
}
