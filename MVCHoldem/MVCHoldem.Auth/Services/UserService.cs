namespace MVCHoldem.Auth.Services
{
    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data;
    using MVCHoldem.Data.Models;

    public class UserService : UserManager<User>, IUserService
    {
        public UserService(IUserStore<User> store)
            : base(store)
        {
        }

        public static UserService Create(IdentityFactoryOptions<UserService> options, IOwinContext context)
        {
            var manager = new UserService(new UserStore<User>(context.Get<MsSqlDbContext>()));

            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

        public User FindById(string userId)
        {
            return UserManagerExtensions.FindById(this, userId);
        }
    }
}