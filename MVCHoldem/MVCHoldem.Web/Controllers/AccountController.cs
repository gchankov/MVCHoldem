namespace MVCHoldem.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.ViewModels;

    [Authorize]
    public class AccountController : Controller
    {
        private readonly ISignInService signInService;
        private readonly IUserService userService;

        public AccountController(ISignInService signInService, IUserService userService)
        {
            this.signInService = signInService;
            this.userService = userService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = this.signInService.Login(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = this.userService.Create(model.Email, model.Password);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Login");
                }

                this.AddErrors(result);
            }
            
            return this.View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return this.RedirectToAction("Index", "Home");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.userService != null)
                {
                    this.userService.Dispose();
                }

                if (this.signInService != null)
                {
                    this.signInService.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }
        #endregion
    }
}