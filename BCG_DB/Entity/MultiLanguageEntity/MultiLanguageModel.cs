namespace BCG_DB.Entity.MultiLanguageEntity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MultiLanguageModel : DbContext
    {
        public MultiLanguageModel()
            : base("name=MultiLanguageModel")
        {
        }

        public virtual DbSet<tblContext> tblContext { get; set; }
        public virtual DbSet<tblLanguages> tblLanguages { get; set; }
        public virtual DbSet<tblResources> tblResources { get; set; }
        public virtual DbSet<tblStaticResources> tblStaticResources { get; set; }
        public virtual DbSet<tblStaticTexts> tblStaticTexts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblContext>()
                .HasMany(e => e.tblResources)
                .WithRequired(e => e.tblContext)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblLanguages>()
                .HasMany(e => e.tblResources)
                .WithRequired(e => e.tblLanguages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblLanguages>()
                .HasMany(e => e.tblStaticResources)
                .WithRequired(e => e.tblLanguages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblStaticTexts>()
                .Property(e => e.StaticText)
                .IsUnicode(false);

            modelBuilder.Entity<tblStaticTexts>()
                .HasMany(e => e.tblStaticResources)
                .WithRequired(e => e.tblStaticTexts)
                .WillCascadeOnDelete(false);
        }
    }
}
