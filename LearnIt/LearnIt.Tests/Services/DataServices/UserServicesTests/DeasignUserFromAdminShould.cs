using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LearnIt.Data.DataModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;

namespace LearnIt.Tests.Services.DataServices.UserServicesTests
{
    [TestClass]
    public class DeasignUserFromAdminShould
    {
        [TestMethod]
        public async Task RemoveUserFromAdmin_WhenIdIsValid()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var userMockOne = new ApplicationUser
            {
                Id = "1",
                UserName = "fake@mail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 4
            };
            List<ApplicationUser> userList = new List<ApplicationUser>()
            {
                userMockOne
            };

            var identityUserRoleMock = new IdentityUserRole
            {
                RoleId = "1",
                UserId = "1"
            };

            var identityRoleMock = new IdentityRole
            {
                Id = "1",
                Name = "Admin",
            };
            List<IdentityRole> rolesList = new List<IdentityRole>()
            {
                identityRoleMock
            };
            var usersMock = new Mock<DbSet<ApplicationUser>>().SetupData(userList);
            var rolesMock = new Mock<DbSet<IdentityRole>>().SetupData(rolesList);

            dbContextMock.SetupGet(x => x.Users).Returns(usersMock.Object);
            dbContextMock.SetupGet(x => x.Roles).Returns(rolesMock.Object);


            UserServices userService = new UserServices(dbContextMock.Object);

            dbContextMock.Object.Users.First().Roles.Add(identityUserRoleMock);
            //act

            await userService.DeasignUserFromAdmin("1");

            //
            Assert.AreEqual(0, dbContextMock.Object.Users.First().Roles.Count);
        }
    }
}
