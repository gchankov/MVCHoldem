namespace MVCHoldem.Services
{
    using System;
    using System.Linq;
    using Bytes2you.Validation;
    using Microsoft.AspNet.Identity;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services.Contracts;

    public class UserService : IUserService
    {
        private readonly IApplicationUserManager applicationUserManager;
        private readonly IEfDbSetWrapper<User> usersDbSet;

        public UserService(IApplicationUserManager applicationUserManager, IEfDbSetWrapper<User> usersDbSet)
        {
            Guard.WhenArgument(applicationUserManager, "applicationUserManager").IsNull().Throw();
            Guard.WhenArgument(usersDbSet, "usersDbSet").IsNull().Throw();

            this.applicationUserManager = applicationUserManager;
            this.usersDbSet = usersDbSet;
        }

        public IdentityResult Create(string userName, string email, string password)
        {
            var user = new User
            {
                UserName = userName,
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

        public User FindByUserName(string userName)
        {
            return this.usersDbSet.AllIncludingDeleted.FirstOrDefault(x => x.UserName == userName);
        }

        public void Dispose()
        {
            this.applicationUserManager.Dispose();
        }
    }
}
