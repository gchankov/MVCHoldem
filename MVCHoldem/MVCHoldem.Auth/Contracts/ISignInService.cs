namespace MVCHoldem.Auth.Contracts
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;
    using MVCHoldem.Data.Models;

    public interface ISignInService : IDisposable
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task SignInAsync(User user, bool isPersistent, bool rememberBrowser);
    }
}