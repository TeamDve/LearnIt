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

namespace LearnIt.Tests.Services.DataServices.PositionServiceTests
{
    [TestClass]
    public class AddPosToUserPossition_Should
    {
        [TestMethod]
        public void SetPossitionToUser_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            var firstPosition = new Position() { Name = "First" };
            List<Position> positionslist = new List<Position>() { firstPosition };
            var positionMock = new Mock<DbSet<Position>>(positionslist);
            // dbContextMock.SetupGet(x => x.DeptRoles).Returns(positionMock.Object);


            List<ApplicationUser> userList = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName="FakeUser",
                    Id = "asd"
                }
            };
            var usersDbSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(userList);
            dbContextMock.SetupGet<IDbSet<ApplicationUser>>(x => x.Users).Returns(usersDbSetMock.Object);
            //Act
            PositionService positionService = new PositionService(dbContextMock.Object);
            //Assert

        }
    }
}
