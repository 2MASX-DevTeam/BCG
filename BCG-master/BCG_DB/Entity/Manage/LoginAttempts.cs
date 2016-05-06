namespace BCG_DB.Entity.Manage
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoginAttempts : DbContext
    {
        public LoginAttempts()
            : base("DefaultConnection")
        {
        }

        public virtual DbSet<tblIPLoginAttempts> tblIPLoginAttempts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
