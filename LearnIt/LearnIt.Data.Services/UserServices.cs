using LearnIt.Data.Context;
using LearnIt.Data.DataModels;
using LearnIt.Data.Models;
using LearnIt.Data.Services.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnIt.Data.Services
{
    public class UserServices : IUserServices
    {

        private readonly ApplicationDbContext dbContext;

        public UserServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext cannot be null");
        }

        public IEnumerable<NameHolder> ReturnAllUserNames()
        {
            IEnumerable<NameHolder> userNames = this.dbContext
                .Users
                .Select
                (u => new NameHolder()
                {
                    Names = u.UserName
                }).ToList();
            return userNames;
        }

        public async Task AsignUserToAdmin(string id)
        {
            var role = this.dbContext.Roles.Single();
            var user = this.dbContext.Users.Single(u => u.Id == id);
            if (user.Roles.Count <= 0)
            {
                user.Roles.Add(new IdentityUserRole()
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });
            }

            await this.ExecuteQuery();
        }

        public async Task DeasignUserFromAdmin(string id)
        {
            var role = this.dbContext.Roles.Single();
            var user = this.dbContext
                .Users
                .Single(u => u.Id == id);
            if (user.Roles.Count >= 1)
            {
                user.Roles.Remove(
                user
                .Roles
                .Single(u => u.UserId == id));
            }

            await this.ExecuteQuery();
        }

        public ApplicationUser ReturnUserByUsername(string username)
        {
            ApplicationUser user = this.dbContext
                .Users
                .Single(u => u.UserName == username);

            return user;
        }

        public bool IsUserAdmin(string id)
        {
            if (this.dbContext.Users.Single(u => u.Id == id).Roles.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task ExecuteQuery()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
