namespace MVCHoldem.Web.Controllers
{
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Enums;
    using MVCHoldem.Web.ViewModels;

    [Authorize]
    public class ManageController : Controller
    {
        private readonly IUserService userService;

        public ManageController(IAuthService authService, IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index(ManageMessageId? message)
        {
            string statusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : string.Empty;

            var userId = User.Identity.GetUserId();
            var model = new ManageViewModel
            {
                HasPassword = this.HasPassword(),
                StatusMessage = statusMessage
            };
            return this.View(model);
        }
        
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return this.View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            
            var result = this.userService.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            this.AddErrors(result);
            return this.View(model);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userService != null)
            {
                this.userService.Dispose();
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

        private bool HasPassword()
        {
            var user = this.userService.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }
#endregion
    }
}