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
    public class BulkCourseDeassignShould
    {
        [TestMethod]
        public void ReturnDefaultView_WhenNoParametersAreGiven()
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
               .WithCallTo(c => c.BulkCourseDeassign())
                   .ShouldRenderDefaultView();

            courseServiceMock.Verify(c => c.ReturnAllCourseNames());
            departmentServiceMock.Verify(d => d.ReturnAllDepartmentNames());
            possitionServiceMock.Verify(p => p.ReturnAllPossitionNames());
        }

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

            var bulkCourseDeassignModelMock = new CourseToPosDepDeassign
            {
                Department = "DepMock",
                Possition = "PosMock",
                CourseName = "SomeCourse",
                DueDate = DateTime.Now,
            };

            //Act & Assert
            adminContoller
                .WithCallTo(c => c.BulkCourseDeassign(bulkCourseDeassignModelMock))
                .ShouldRedirectToRoute("");

            courseServiceMock.Verify(c => c.DeassignExistingCourseToPosAndDept(
                bulkCourseDeassignModelMock.CourseName,
                bulkCourseDeassignModelMock.Department,
                bulkCourseDeassignModelMock.Possition,
                bulkCourseDeassignModelMock.DueDate), Times.Once);

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

            var bulkCourseDeassignModelMock = new CourseToPosDepDeassign
            {
                Department = "DepMock",
                Possition = "PosMock",
                CourseName = "SomeCourse",
                DueDate = DateTime.Now
            };

            courseServiceMock.Setup(c => c.DeassignExistingCourseToPosAndDept(
                bulkCourseDeassignModelMock.CourseName,
                bulkCourseDeassignModelMock.Department,
                bulkCourseDeassignModelMock.Possition,
                bulkCourseDeassignModelMock.DueDate))
                .Throws<ArgumentNullException>();

            //Act & Assert
            adminContoller
                .WithCallTo(c => c.BulkCourseDeassign(bulkCourseDeassignModelMock))
                .ShouldRenderDefaultView().WithModel<CourseToPosDepDeassign>();

            courseServiceMock.Verify(c => c.ReturnAllCourseNames());
            departmentServiceMock.Verify(d => d.ReturnAllDepartmentNames());
            possitionServiceMock.Verify(p => p.ReturnAllPossitionNames());
        }
    }
}
