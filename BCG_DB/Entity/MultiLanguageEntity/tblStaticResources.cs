namespace BCG_DB.Entity.MultiLanguageEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblStaticResources
    {
        [Key]
        public int IdStatic { get; set; }

        public int IdLanguage { get; set; }

        public int IdStaticText { get; set; }

        [StringLength(100)]
        public string StaticName { get; set; }

        public DateTime? UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual tblLanguages tblLanguages { get; set; }

        public virtual tblStaticTexts tblStaticTexts { get; set; }
    }
}
