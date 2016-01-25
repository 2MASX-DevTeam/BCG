using System.Data.Entity;
using BCG_Portal_Data.Migrations;
using BCG_Portal_Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BCG_Portal_Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BCGPortalDbContext : IdentityDbContext<User>
    {
        // Your context has been configured to use a 'BCGPortalDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BCG_Portal_Data.BCGPortalDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BCGPortalDbContext' 
        // connection string in the application configuration file.
        public BCGPortalDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BCG_Portal_Data.BCGPortalDbContext, Configuration>());
        }

        public static BCG_Portal_Data.BCGPortalDbContext Create()
        {
            return new BCG_Portal_Data.BCGPortalDbContext();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}


