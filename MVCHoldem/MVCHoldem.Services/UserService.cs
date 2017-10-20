namespace MVCHoldem.Services
{
    using System;
    using Bytes2you.Validation;
    using Microsoft.AspNet.Identity;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services.Contracts;

    public class UserService : IUserService
    {
        private readonly IApplicationUserManager applicationUserManager;

        public UserService(IApplicationUserManager applicationUserManager)
        {
            Guard.WhenArgument(applicationUserManager, "applicationUserManager").IsNull().Throw();

            this.applicationUserManager = applicationUserManager;
        }

        public IdentityResult Create(string email, string password)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                CreatedOn = DateTime.Now
            };

            return this.applicationUserManager.CreateAsync(user, password).Result;
        }

        public IdentityResult ChangePassword(string userId, string currentPassword, string newPassword)
        {
            return this.applicationUserManager.ChangePasswordAsync(userId, currentPassword, newPassword).Result;
        }
        
        public User FindById(string userId)
        {
            return this.applicationUserManager.FindById(userId);
        }

        public void Dispose()
        {
            this.applicationUserManager.Dispose();
        }
    }
}
