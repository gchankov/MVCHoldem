namespace MVCHoldem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCHoldem.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<EfDbContext>
    {
        private const string AdministratorUserName = "admin";
        private const string AdministratorEmail = "admin@admin.com";
        private const string AdministratorPassword = "123qwe";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(EfDbContext context)
        {
            this.SeedUsers(context);
            this.SeedPosts(context);
            base.Seed(context);       
        }

        private void SeedUsers(EfDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorEmail,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };
                userManager.Create(user, AdministratorPassword);

                userManager.AddToRole(user.Id, "Admin");
            }
        }

        private void SeedPosts(EfDbContext context)
        {
            if (!context.Posts.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var post = new Post()
                    {
                        Title = "Post " + i,
                        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sit amet...",
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sit amet lobortis nibh. Nullam bibendum, tortor quis porttitor fringilla, eros risus consequat orci, at scelerisque mauris dolor sit amet nulla. Vivamus turpis lorem, pellentesque eget enim ut, semper faucibus tortor. Aenean malesuada laoreet lorem.",
                        Author = context.Users.First(x => x.UserName == AdministratorUserName),
                        CreatedOn = DateTime.Now
                    };

                    context.Posts.Add(post);
                }
            }
        }
    }
}
