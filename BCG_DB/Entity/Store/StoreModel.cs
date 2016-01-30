namespace BCG_DB.Entity.Store
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StoreModel : DbContext
    {
        public StoreModel()
            : base("name=StoreModel")
        {
        }

        public virtual DbSet<tblCurrency> tblCurrencies { get; set; }
        public virtual DbSet<tblCustomerReview> tblCustomerReviews { get; set; }
        public virtual DbSet<tblDiscount> tblDiscounts { get; set; }
        public virtual DbSet<tblOrder> tblOrders { get; set; }
        public virtual DbSet<tblOrder1> tblOrders1 { get; set; }
        public virtual DbSet<tblOrderStatus> tblOrderStatuses { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblQuantity> tblQuantities { get; set; }
        public virtual DbSet<tblShopingCart> tblShopingCarts { get; set; }
        public virtual DbSet<tblShopperGroup> tblShopperGroups { get; set; }
        public virtual DbSet<tblShopper> tblShoppers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCurrency>()
                .Property(e => e.CurrencyValue)
                .HasPrecision(4, 2);

            modelBuilder.Entity<tblCurrency>()
                .HasMany(e => e.tblProducts)
                .WithRequired(e => e.tblCurrency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblOrder1>()
                .HasMany(e => e.tblOrders)
                .WithRequired(e => e.tblOrder1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblOrderStatus>()
                .HasMany(e => e.tblOrders)
                .WithRequired(e => e.tblOrderStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblProduct>()
                .HasMany(e => e.tblCustomerReviews)
                .WithRequired(e => e.tblProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblProduct>()
                .HasMany(e => e.tblOrders)
                .WithRequired(e => e.tblProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblProduct>()
                .HasMany(e => e.tblShopingCarts)
                .WithRequired(e => e.tblProduct)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblQuantity>()
                .HasMany(e => e.tblOrders)
                .WithRequired(e => e.tblQuantity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblQuantity>()
                .HasMany(e => e.tblShopingCarts)
                .WithRequired(e => e.tblQuantity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblShopper>()
                .HasMany(e => e.tblCustomerReviews)
                .WithRequired(e => e.tblShopper)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblShopper>()
                .HasMany(e => e.tblOrders)
                .WithRequired(e => e.tblShopper)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblShopper>()
                .HasMany(e => e.tblShopingCarts)
                .WithRequired(e => e.tblShopper)
                .WillCascadeOnDelete(false);
        }
    }
}
