using LearnIt.Areas.Admin.Controllers;
using LearnIt.Areas.Admin.Models;
using LearnIt.Data.Enums;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Admin.Contrellers.AdminControllerTests
{
    [TestClass]
    public class EditUserShould
    {
        [TestMethod]
        public void RedirectToActionAndAssignToAdmin_WhenModelIsAdmin()
        {
            //Arrange
            var jsonParserMock = new Mock<IJsonParserService>();
            var courseServiceMock = new Mock<ICourseService>();
            var userServicesMock = new Mock<IUserServices>();
            var departmentServiceMock = new Mock<IDepartmenService>();
            var possitionServiceMock = new Mock<IPositionService>();

            var adminContoller = new AdminController(
                jsonParserMock.Object,
                courseServiceMock.Object,
                userServicesMock.Object,
                departmentServiceMock.Object,
                possitionServiceMock.Object);

            var userMock = new UserViewModel
            {
                Department = "DepMock",
                Position = "PosMock",
                Username = "NameMock",
                Id = "someId",
                IsAdmin = true
            };

            //Act & Assert
            adminContoller
                .WithCallTo(c => c.EditUser(userMock))
                .ShouldRedirectToRoute("");

            userServicesMock.Verify(u => u.AssignUserToAdmin(userMock.Id), Times.Once);
        }

        [TestMethod]
        public void RedirectToActionAndDeassignToAdmin_WhenModelIsNotAdmin()
        {
            //Arrange
            var jsonParserMock = new Mock<IJsonParserService>();
            var courseServiceMock = new Mock<ICourseService>();
            var userServicesMock = new Mock<IUserServices>();
            var departmentServiceMock = new Mock<IDepartmenService>();
            var possitionServiceMock = new Mock<IPositionService>();

            var adminContoller = new AdminController(
                jsonParserMock.Object,
                courseServiceMock.Object,
                userServicesMock.Object,
                departmentServiceMock.Object,
                possitionServiceMock.Object);

            var userMock = new UserViewModel
            {
                Department = "DepMock",
                Position = "PosMock",
                Username = "NameMock",
                Id = "someId",
                IsAdmin = false
            };

            //Act & Assert
            adminContoller
                .WithCallTo(c => c.EditUser(userMock))
                .ShouldRedirectToRoute("");

            userServicesMock.Verify(u => u.DeasignUserFromAdmin(userMock.Id), Times.Once);
        }
    }
}
