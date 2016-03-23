using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BCG_Portal_Models
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

    /* End */
}
