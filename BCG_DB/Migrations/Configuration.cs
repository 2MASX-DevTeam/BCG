namespace BCG_DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BCG_DB.Entity.Manage.LoginAttempts>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BCG_DB.Entity.Manage.LoginAttempts";
        }

        protected override void Seed(BCG_DB.Entity.Manage.LoginAttempts context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
