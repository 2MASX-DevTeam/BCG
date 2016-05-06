using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCG_Manage.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser 
    {
        public DateTime BirthDate { get; set; }
        public DateTime DateCreated { get; internal set; }
        public DateTime DateModified { get; internal set; }
        public string FirstName { get; set;  }
        public string LastName { get; set; }
        public virtual ICollection<tblProfilePicturesAdmins>  TblProfilePictures{ get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

      //  public System.Data.Entity.DbSet<BCG_Manage.Models.ApplicationUser> ApplicationUsers { get; set; }
    }

    public class tblProfilePicturesAdmins
    {
        [Key]
        public int IdProfilePicture { get; set; }

        [Required]
        [StringLength(500)]
        public string PicturePath { get; set; }

        [Required]
        public bool IsProfile { get; set; }

        [Required]
        public bool IsVisible { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}