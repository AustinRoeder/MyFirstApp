using GitFirstApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GitFirstApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GitFirstApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GitFirstApp.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "austinjroeder@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "austinjroeder@gmail.com",
                    Email = "austinjroeder@gmail.com",
                    FirstName = "Austin",
                    LastName = "Roeder",
                    DisplayName = "Austin"
                }, "Purdue96!");
            }
            var userID = userManager.FindByEmail("austinjroeder@gmail.com").Id;
            userManager.AddToRole(userID, "Admin");
        }
    }
}
