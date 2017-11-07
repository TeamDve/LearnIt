using LearnIt.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using LearnIt.Data.Context;
using System.Linq;

namespace LearnIt.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LearnIt.Data.Context.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole() { Name = "Admin" });
                context.SaveChanges();
                var role = context.Roles.Single();
                var user = context.Users.Single();
                user.Roles.Add(new IdentityUserRole()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
                context.SaveChanges();
            }
        }
    }
}
