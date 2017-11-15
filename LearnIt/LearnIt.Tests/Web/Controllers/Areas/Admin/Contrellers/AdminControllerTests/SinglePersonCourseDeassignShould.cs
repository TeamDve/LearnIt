using LearnIt.Areas.Admin.Controllers;
using LearnIt.Areas.Admin.Models;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Admin.Contrellers.AdminControllerTests
{
    [TestClass]
    public class SinglePersonCourseDeassignShould
    {
        [TestMethod]
        public void RedirectToAssignCourse_WhenParamsAreCorrect()
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

            var singleCourseDeassignModelMock = new CourseToUserDeassign
            {
                Username = "fake@user.com",
                CourseName = "SomeCourse",
                DueDate = DateTime.Now,
            };

            //Act & Assert
            adminContoller
                .WithCallTo(c => c.SinglePersonCourseDeassign(singleCourseDeassignModelMock))
                .ShouldRedirectToRoute("");

            courseServiceMock.Verify(c => c.DeassignCourseFromUser(
                singleCourseDeassignModelMock.CourseName,
                singleCourseDeassignModelMock.Username,
                singleCourseDeassignModelMock.DueDate), Times.Once);

        }

        [TestMethod]
        public void RedirectToDefaultView_WhenNoParamsAreGiven()
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

            //Act & Assert
            adminContoller
                .WithCallTo(c => c.SinglePersonCourseDeassign())
                .ShouldRenderDefaultView();

            courseServiceMock.Verify(c => c.ReturnAllCourseNames(), Times.Once);
            userServicesMock.Verify(u => u.ReturnAllUserNames(), Times.Once);
        }

        [TestMethod]
        public void ReturDefaultView_WhenParamsAreNotCorrect()
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

            var singleCourseDeassignModelMock = new CourseToUserDeassign
            {
                Username = "fake@user.com",
                CourseName = "SomeCourse",
                DueDate = DateTime.Now,
            };

            courseServiceMock.Setup(c => c.DeassignCourseFromUser(
                singleCourseDeassignModelMock.CourseName,
                singleCourseDeassignModelMock.Username,
                singleCourseDeassignModelMock.DueDate))
                .Throws<ArgumentNullException>();
            //Act & Assert
            adminContoller
                .WithCallTo(c => c.SinglePersonCourseDeassign(singleCourseDeassignModelMock))
                .ShouldRenderDefaultView().WithModel<CourseToUserDeassign>();

            courseServiceMock.Verify(c => c.ReturnAllCourseNames(), Times.Once);
            userServicesMock.Verify(u => u.ReturnAllUserNames(), Times.Once);
        }
    }
}
