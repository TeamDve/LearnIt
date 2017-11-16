using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LearnIt.Data.DataModels;

namespace LearnIt.Tests.Services.DataServices.UserServicesTests
{
    [TestClass]
    public class ReturnAllUserNamesShould
    {
        [TestMethod]
        public void ReturnAnEnumerationOfAllUserNames()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var userMockOne = new ApplicationUser
            {
                Id = "1",
                UserName="fake@mail.com",
                EmailConfirmed=true,
                PhoneNumberConfirmed=true,
                TwoFactorEnabled=true,
                LockoutEnabled=true,
                AccessFailedCount=4
            };

            var userMock2 = new ApplicationUser
            {
                Id = "2",
                UserName = "notfake@mail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 4
            };

            List<ApplicationUser> userList = new List<ApplicationUser>()
            {
                userMockOne,userMock2
            };

            var usersMock = new Mock<DbSet<ApplicationUser>>().SetupData(userList);
            dbContextMock.SetupGet(x => x.Users).Returns(usersMock.Object);
            
            UserServices userService = new UserServices(dbContextMock.Object);

            //Act

            var listOfUserNamesMock = userService.ReturnAllUserNames();

            //Assert
            Assert.AreEqual(2, listOfUserNamesMock.Count());
            Assert.IsInstanceOfType(listOfUserNamesMock, typeof(IEnumerable<NameHolder>));
        }
    }
}
