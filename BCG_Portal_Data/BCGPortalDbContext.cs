using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCG_Portal_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BCG_Portal_Data
{
 
    public class BCGPortalDbContext : IdentityDbContext<User>
    {
        public BCGPortalDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static BCGPortalDbContext Create()
        {
            return new BCGPortalDbContext();
        }

        //  public System.Data.Entity.DbSet<BCG_Manage.Models.ApplicationUser> ApplicationUsers { get; set; }

        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<CartItem> CartItems { get; set; }
    }
}
