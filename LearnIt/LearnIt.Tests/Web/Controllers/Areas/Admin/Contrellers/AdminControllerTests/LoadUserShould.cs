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
    public class LoadUserShould
    {
        [TestMethod]
        public void ReturnDefaultView()
        {
            ////Arrange
            //var jsonParserMock = new Mock<IJsonParserService>();
            //var courseServiceMock = new Mock<ICourseService>();
            //var userServicesMock = new Mock<IUserServices>();
            //var departmentServiceMock = new Mock<IDepartmenService>();
            //var possitionServiceMock = new Mock<IPositionService>();

            //var adminContoller = new AdminController(
            //    jsonParserMock.Object,
            //    courseServiceMock.Object,
            //    userServicesMock.Object,
            //    departmentServiceMock.Object,
            //    possitionServiceMock.Object);

            //string name = "someMockName";
            //string id = "someMockId";
            //CourseStatus courseStatusPending = CourseStatus.Pending;
            //CourseStatus courseStatusStarted = CourseStatus.Started;
            //CourseStatus courseStatusCompleted = CourseStatus.Completed;
            ////Act & Assert
            //adminContoller
            //    .WithCallTo(c => c.LoadUser(name))
            //    .ShouldRenderPartialView("_LoadUser").WithModel<UserViewModel>(Assert.AreEqual());

            //userServicesMock.Verify(u => u.ReturnUserByUsername(name), Times.Once);
            //userServicesMock.Verify(u => u.IsUserAdmin(id),Times.Once);
            //courseServiceMock.Verify(c => c.GetUsersCourseInfoByStatus(name, courseStatusPending), Times.Once);
            //courseServiceMock.Verify(c => c.GetUsersCourseInfoByStatus(name, courseStatusCompleted), Times.Once);
            //courseServiceMock.Verify(c => c.GetUsersCourseInfoByStatus(name, courseStatusStarted), Times.Once);
        }
    }
}
