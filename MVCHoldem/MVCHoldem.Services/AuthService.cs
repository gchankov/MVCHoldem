namespace MVCHoldem.Services
{
    using Microsoft.AspNet.Identity.Owin;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Services.Contracts;

    public class AuthService : IAuthService
    {
        private readonly IApplicationSignInManager applicationSignInManager;
        private readonly IApplicationUserManager applicationUserManager;

        public AuthService(IApplicationSignInManager applicationSignInManager, IApplicationUserManager applicationUserManager)
        {
            this.applicationSignInManager = applicationSignInManager;
            this.applicationUserManager = applicationUserManager;
        }

        public SignInStatus Login(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            return this.applicationSignInManager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout).Result;
        }

        public void Login(string userId, bool isPersistent, bool rememberBrowser)
        {
            var user = this.applicationUserManager.FindById(userId);
            this.applicationSignInManager.SignInAsync(user, isPersistent, rememberBrowser).Wait();
        }

        public void Dispose()
        {
            this.applicationSignInManager.Dispose();   
        }
    }
}
