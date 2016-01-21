namespace BCG_Manage.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using BCG_DB;
    using BCG_DB.Entity.Manage;
    using Tools;
    using Models;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    public class HomeController : BaseController
    {
        #region Constructors
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        #endregion

        private LoginAttempts db = new LoginAttempts();

        [HttpGet]

        public ActionResult Index()
        {
            try
            {
                string ip = IPAddress.GetClientIPAddress(System.Web.HttpContext.Current);
                string browserVersion = Request.UserAgent;

                var model = new tblIPLoginAttempts()
                {
                    IPAdress = ip,
                    UserAgend = browserVersion,
                    DateChanged = DateTime.Now,
                    DateCreated = DateTime.Now,
                    UserName = "SYSTEM"
                };

                db.tblIPLoginAttempts.Add(model);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                SendExceptionToAdmin(ex.ToString());
            }
       
            return View();
        }


        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "ManageSite");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        
    }
}