using BCG_Portal.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCG_Portal.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }

        public IDbSet<IPLoginAttempt> IPLoginAttempts {  get; set; }

    }
}
