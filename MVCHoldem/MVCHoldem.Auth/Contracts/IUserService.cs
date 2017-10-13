namespace MVCHoldem.Auth.Contracts
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using MVCHoldem.Data.Models;

    public interface IUserService : IDisposable
    {
        Task<IdentityResult> CreateAsync(User user, string password);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<User> FindByIdAsync(string userId);

        User FindById(string userId);
    }
}
