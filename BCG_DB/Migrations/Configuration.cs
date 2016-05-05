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
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(BCG_DB.Entity.Manage.LoginAttempts context)
        {
            
        }
    }
}
