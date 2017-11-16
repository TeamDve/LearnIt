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
    public class BulkCourseAssignShould
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
               .WithCallTo(c => c.BulkCourseAssign())
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

            var bulkCourseAsignModelMock = new CourseToPosDep
            {
                Department = "DepMock",
                Possition = "PosMock",
                CourseName = "SomeCourse",
                DueDate = DateTime.Now,
                IsMandatory = true
            };

            //Act & Assert
            adminContoller
                .WithCallTo(c => c.BulkCourseAssign(bulkCourseAsignModelMock))
                .ShouldRedirectToRoute("");

            courseServiceMock.Verify(c => c.AssignExistingCourseToPosAndDept(
                bulkCourseAsignModelMock.CourseName,
                bulkCourseAsignModelMock.Department,
                bulkCourseAsignModelMock.Possition,
                bulkCourseAsignModelMock.DueDate,
                bulkCourseAsignModelMock.IsMandatory), Times.Once);

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

            var bulkCourseAsignModelMock = new CourseToPosDep
            {
                Department = "DepMock",
                Possition = "PosMock",
                CourseName = "SomeCourse",
                DueDate = DateTime.Now,
                IsMandatory = true
            };

            courseServiceMock.Setup(c => c.AssignExistingCourseToPosAndDept(
                bulkCourseAsignModelMock.CourseName,
                bulkCourseAsignModelMock.Department,
                bulkCourseAsignModelMock.Possition,
                bulkCourseAsignModelMock.DueDate,
                bulkCourseAsignModelMock.IsMandatory))
                .Throws<ArgumentNullException>();
            //Act & Assert
            adminContoller
                .WithCallTo(c => c.BulkCourseAssign(bulkCourseAsignModelMock))
                .ShouldRenderDefaultView().WithModel<CourseToPosDep>();

            courseServiceMock.Verify(c => c.ReturnAllCourseNames());
            departmentServiceMock.Verify(d => d.ReturnAllDepartmentNames());
            possitionServiceMock.Verify(p => p.ReturnAllPossitionNames());
        }
    }
}
