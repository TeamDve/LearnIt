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
    public class RetrunAllPossitionNames_Should
    {
        [TestMethod]
        public void ReturnAllPossitionsInDb_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var firstPosition = new Position() { Name = "First" };
            var secondPosition = new Position() { Name = "second" };
            var thirdPosition = new Position() { Name = "Third" };
            List<Position> positionslist = new List<Position>() { firstPosition, secondPosition, thirdPosition };

            var positionMock = new Mock<DbSet<Position>>().SetupData(positionslist);
            dbContextMock.SetupGet(x => x.DeptRoles).Returns(positionMock.Object);
            //Act
            PositionService positionService = new PositionService(dbContextMock.Object);

            var testObject = positionService.ReturnAllPossitionNames();
            //Assert
            Assert.AreEqual(3, testObject.Count());

        }
    }
}
