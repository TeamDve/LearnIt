using LearnIt.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;


namespace LearnIt.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LearnIt.Data.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LearnIt.Data.Context.ApplicationDbContext";
        }

        protected override void Seed(LearnIt.Data.Context.ApplicationDbContext context)
        {
            //The almighty dollar
            context.Users.AddOrUpdate(new ApplicationUser() { UserName = "headAdmin", })
            context.Roles.AddOrUpdate(new IdentityRole() { Name = "Admin"})
        }
    }
}
