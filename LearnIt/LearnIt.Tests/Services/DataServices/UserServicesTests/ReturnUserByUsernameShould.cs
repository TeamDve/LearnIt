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
    public class ReturnUserByUsernameShould
    {
        [TestMethod]
        public void ReturnUser_WhenUsernameIsCorrect()
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

            var usersMock = new Mock<DbSet<ApplicationUser>>().SetupData(userList);
            dbContextMock.SetupGet(x => x.Users).Returns(usersMock.Object);

            UserServices userService = new UserServices(dbContextMock.Object);

            //Act
            var resultUser = userService.ReturnUserByUsername("fake@mail.com");

            //Assert
            Assert.AreEqual(resultUser, userMockOne);
        }
    }
}
