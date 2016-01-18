using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BCG_Manage.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace BCG_Manage.Controllers
{
    public class RolesController : BaseController
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: /Roles/ViewAllRoles
        [HttpGet]
        public ActionResult ViewAllRoles()
        {
            var roles = context.Roles.ToList();
            return View(roles);
        }


        // GET: /Roles/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                TempData["ResultSuccess"] = "Role created successfully !";
                return RedirectToAction("ViewAllRoles");
            }
            catch
            {
                TempData["ResultError"] = "Error in creating role!";
                return RedirectToAction("ViewAllRoles");
            }
        }




        // GET: /Roles/Edit/5
        [HttpGet]
        public ActionResult EditRole(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                TempData["ResultSuccess"] = "Role edited successfully !";
                return RedirectToAction("ViewAllRoles");
            }
            catch
            {
                TempData["ResultError"] = "Error in editing role!";
                return RedirectToAction("ViewAllRoles");
            }
        }

        [HttpGet]
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>

            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            try
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
              //  var account = new AccountController();
             //   account.UserManager.AddToRoleAsync(user.Id, RoleName);


                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var idResult = um.AddToRole(user.Id, RoleName);

                // prepopulat roles for the view dropdown 
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;

                TempData["ResultSuccess"] = "Role added successfully !";

                return View("ManageUserRoles");
            }

           
            catch (Exception)
            {
                TempData["ResultError"] = "Error in adding role!";
                return RedirectToAction("ManageUserRoles");
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var account = new AccountController();


             //   ViewBag.RolesForThisUser = account.UserManager.GetRoles(user.Id);

                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ViewBag.RolesForThisUser = um.GetRoles(user.Id);


                // prepopulat roles for the view dropdown 
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }


            return View("ManageUserRoles");
        }

        // GET: /Roles/Delete
        [HttpGet]
        public ActionResult Delete(string RoleName)
        {
            try
            {
                var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                context.Roles.Remove(thisRole);
                context.SaveChanges();

                TempData["ResultSuccess"] = "Role deleted successfully !";
                return RedirectToAction("ViewAllRoles");
            }
            catch (Exception)
            {
                TempData["ResultError"] = "Error in editing role!";
                return RedirectToAction("ViewAllRoles");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));


            if (um.IsInRole(user.Id, RoleName))
            {
                um.RemoveFromRole(user.Id, RoleName);

                TempData["ResultSuccess"] = "Role removed from this user successfully!";
            }
            else
            {
                TempData["ResultError"] = "This user doesn't belong to selected role.";

            }
            // prepopulat roles for the view dropdown 
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;


            return View("ManageUserRoles");
        }

    }
}