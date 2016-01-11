namespace BCG_DB.Entity.MultiLanguageEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblLanguages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLanguages()
        {
            tblResources = new HashSet<tblResources>();
            tblStaticResources = new HashSet<tblStaticResources>();
        }

        [Key]
        public int IdLanguage { get; set; }

        [Required]
        [StringLength(50)]
        public string Language { get; set; }

        [Required]
        [StringLength(10)]
        public string Initials { get; set; }

        [Required]
        [StringLength(20)]
        public string Culture { get; set; }

        [StringLength(100)]
        public string Picture { get; set; }

        public bool IsActive { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? Datechanged { get; set; }

        public DateTime? DateCreated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblResources> tblResources { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStaticResources> tblStaticResources { get; set; }
    }
}
