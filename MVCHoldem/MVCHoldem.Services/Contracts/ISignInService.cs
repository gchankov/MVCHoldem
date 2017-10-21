namespace MVCHoldem.Services.Contracts
{
    using System;
    using Microsoft.AspNet.Identity.Owin;

    public interface ISignInService : IDisposable
    {
        SignInStatus Login(string userName, string password, bool isPersistent, bool shouldLockout);

        void Login(string userId, bool isPersistent, bool rememberBrowser);
    }
}