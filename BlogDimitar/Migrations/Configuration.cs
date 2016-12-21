namespace BlogDimitar.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<BlogDimitar.Models.BlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BlogDbContext context)
        {
            if (!context.Roles.Any())
            {
                this.CreateRole(context, "Admin");
                this.CreateRole(context, "User");
            }
            if (!context.Users.Any())
            {
                this.CreateUser(context, "admin@admin.com", "Admin", "1a");
                this.SetRoleToUser(context, "admin@admin.com", "Admin");
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private void SetRoleToUser(BlogDbContext context, string email, string role)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = context.Users
                .Where(u => u.Email == email)
                .First();

            var result = userManager.AddToRole(user.Id, role);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }

        private void CreateRole(BlogDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var result = roleManager.Create(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
        private void CreateUser(BlogDbContext context, string email, string fullName, string password)
        {
            // Create user manager
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // Set user manager pasword validator
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 2,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false,
                RequireNonLetterOrDigit = false
            };
            // Create user object
            var admin = new ApplicationUser
            {
                UserName = email,
                FullName = fullName,
                Email = email,
            };
            // Create user
            var result = userManager.Create(admin, password);
            // Validate result
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
    }
}
