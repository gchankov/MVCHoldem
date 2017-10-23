namespace MVCHoldem.Services.Contracts
{
    using System;
    using Microsoft.AspNet.Identity;
    using MVCHoldem.Data.Models;

    public interface IUserService : IDisposable
    {
        IdentityResult ChangePassword(string userId, string currentPassword, string newPassword);

        IdentityResult Create(string userName, string email, string password);

        User FindById(string userId);

        User FindByUserName(string userName);
    }
}