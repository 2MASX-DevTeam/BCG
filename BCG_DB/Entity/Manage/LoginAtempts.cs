namespace BCG_DB.Entity.Manage
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoginAtempts : DbContext
    {
        public LoginAtempts()
            : base("DefaultConnection")
        {
        }

        public virtual DbSet<tblIPLoginAtemts> tblIPLoginAtemts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
