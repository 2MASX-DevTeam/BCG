using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCG_Portal.Models;
using System.Data.Entity.Validation;
//using SendMailHelper;
using System.Text;
using System.Text.RegularExpressions;
using BCG_Portal.Helpers;
using System.Web.Security;
using System.Threading.Tasks;
using System.Configuration;

namespace BCG_Portal.Controllers
{
    public class LoginRegisterController : Controller
    {
        //private ExceptionToAdmin mail = new ExceptionToAdmin();
        private EmailConfirmationHandler sendConfirmMail = new EmailConfirmationHandler();
        private PasswordHandler secureThePass = new PasswordHandler();

        //private BCGEntities db = new BCGEntities();

        // GET: LoginRegister
        public ActionResult Index()
        {
            return View();
        }

        #region "Login User"
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.ShowDiv = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(tblShopper logUser)
        {
            /* Validate the  custom input fields*/
            if (!ValidateLoginInput(logUser))
            {
                return View();
            }
            if (!CheckUserData(logUser))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        private bool ValidateLoginInput(tblShopper logUser)
        {
            /* Secure empty fields */
           if (string.IsNullOrEmpty(logUser.UserName) || string.IsNullOrWhiteSpace(logUser.UserName)
                || string.IsNullOrEmpty(logUser.Password) || string.IsNullOrWhiteSpace(logUser.Password))
           {
                ViewBag.DivClass = "alert alert-danger";
                ViewBag.ShowErrorDiv = true;
                ViewBag.ErrorMessage = "All fields are required!";
                return false;
           }
            return true;
            /* Check if the user exists in the database and compare the password */

        }

        private bool CheckUserData(tblShopper logUser)
        {
            using (var db = new BCGEntities())
            {
                var pass = secureThePass.Encrypt(logUser.Password.Trim());
                var user = db.tblShoppers.FirstOrDefault(u => u.UserName == logUser.UserName && u.Password == pass);
                if (user == null)
                {
                    ViewBag.DivClass = "alert alert-danger";
                    ViewBag.ShowErrorDiv = true;
                    ViewBag.ErrorMessage = "Wrong Username or Password! Try again.";
                    return false;
                }
            }
            return true;
        }

        #endregion "Login User"

        #region "Register User"
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.ShowDiv = false;
            return View();
        }

        //POST: /Account/Register
        [HttpPost]
        public ActionResult Register(tblShopper regUser)
        {
            #region "Secure Input Fields"
            /* Validate the  custom input fields*/
            if (!ValidateRegisterInput(regUser))
            {
                return View();
            }
        
            /* Check if the user request for registration exist in the database. Is user registered? */
            if (!IsUserExisting(regUser))
            {
                return View();
            }
            #endregion "Secure Input Fields"

            try
            {
                bool isOk = false;
                var cryptPass = secureThePass.Encrypt(regUser.Password.Trim());
                string confirmationToken = CreateConfirmationToken(cryptPass);

                if (ModelState.IsValid)
                {
                    using (var db = new BCGEntities())
                    {
                        var user = db.tblShoppers.Create();

                        user.UserName = regUser.UserName;
                        user.Password = cryptPass;
                        user.Email = regUser.Email;
                        user.DateCreated = DateTime.Now;
                        user.ConfirmationToken = confirmationToken;
                        db.tblShoppers.Add(user);
                        db.SaveChanges();

                        isOk = true;
                    }
                    if (isOk)
                    {
                       // TO DO....
                        /* Create template for the email body message */
                        string body = "Send you a confirmation token => " + confirmationToken;

                        sendConfirmMail.SendEmailConfirmation(regUser.UserName, regUser.Email, body);
                        return RedirectToAction("Index", "RegisterUserAdminPanel");
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (DbEntityValidationException e)
            {
                StringBuilder br = new StringBuilder();
                foreach (var eve in e.EntityValidationErrors)
                {
                    var jo = string.Format("Entity of type [{0}]  in state [{1}] has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    br.AppendLine(jo);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        var jo2 = string.Format("- Property: [{0}], Error: [{1}]",
                            ve.PropertyName, ve.ErrorMessage);
                        br.AppendLine(jo2);
                    }
                    /* Send info whats wrong */
                  //  mail.SendExceptionToAdmin(br.ToString());
                }
            }
            catch(Exception Ex)
            {
                /* Send info whats wrong */
                ViewBag.DivClass = "alert alert-danger";
                ViewBag.ShowErrorDiv = true;
                ViewBag.ErrorMessage = "Something get wrong! Unable to save the data! Try Again!";
                string error = Ex.ToString();
                //mail.SendExceptionToAdmin("BCG Portal: Error in registration form => " + error);
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        private string CreateConfirmationToken(string secretValue)
        {
            Guid guid = new Guid(ConfigurationManager.AppSettings["TokenGuid"]);
            return guid.ToString();
        }

        private bool ValidateRegisterInput(tblShopper regUser)
        {
            if (string.IsNullOrEmpty(regUser.UserName) || string.IsNullOrWhiteSpace(regUser.UserName)
                || string.IsNullOrEmpty(regUser.Password) || string.IsNullOrWhiteSpace(regUser.Password))
            {
                ViewBag.DivClass = "alert alert-danger";
                ViewBag.ShowErrorDiv = true;
                ViewBag.ErrorMessage = "All fields are required!";
                return false;
            }

            if (string.IsNullOrEmpty(regUser.Email) || string.IsNullOrWhiteSpace(regUser.Email))
            {
                 ViewBag.DivClass = "alert alert-danger";
                 ViewBag.ShowErrorDiv = true;
                ViewBag.ErrorMessage = "Email is required!";
                return false;
            }

            Regex rgx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = rgx.Match(regUser.Email);
            if (!match.Success)
            {
                ViewBag.DivClass = "alert alert-danger";
                ViewBag.ShowErrorDiv = true;
                ViewBag.ErrorMessage = "Incorrect e-mail!";
                return false;
            }
            return true;
        }

        private bool IsUserExisting(tblShopper regUser)
        {
            using (var db = new BCGEntities())
            {
               var user = db.tblShoppers.FirstOrDefault(u => u.Email == regUser.Email.Trim());
           
                if (user != null)
                {
                    ViewBag.DivClass = "alert alert-danger";
                    ViewBag.ShowErrorDiv = true;
                    ViewBag.ErrorMessage = "This email is assigned to existing user!";
                    return false;
                }
            }
            return true;
        }
        #endregion "Register User"

        /* END */
    }
}