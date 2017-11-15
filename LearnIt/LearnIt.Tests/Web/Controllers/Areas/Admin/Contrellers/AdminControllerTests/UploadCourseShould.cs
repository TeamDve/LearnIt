using LearnIt.Areas.Admin.Controllers;
using LearnIt.Areas.Admin.Models;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Web;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Admin.Contrellers.AdminControllerTests
{
    [TestClass]
    public class UploadCourseShould
    {
        [TestMethod]
        public void ReturnDefaulView_WhenNoParametersAreGiven()
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
               .WithCallTo(c => c.UploadCourse())
               .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void RedirectToAssignCourse_WhenParameterIsNull()
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

            HttpPostedFileBase fileMock = null;

            //Act & Assert
            adminContoller
               .WithCallTo(c => c.UploadCourse(fileMock))
               .ShouldRedirectToRoute("");
        }

        [TestMethod]
        public void RedirectToDefault_WhenParametersAreGiven()
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

            var fileMock = new Mock<HttpPostedFileBase>();

            //var binaryReaderMock = new BinaryReader(fileMock.Object.InputStream);

            //var binDataMock= binaryReaderMock.ReadBytes(fileMock.Object.ContentLength);

            //var testMock = jsonParserMock.Object.Execute(binDataMock);

            //Act & Assert
            adminContoller
               .WithCallTo(c => c.UploadCourse(fileMock.Object))
               .ShouldRenderDefaultView();

            //courseServiceMock.Verify(c => c.AddCourseToDb(testMock), Times.Once);
        }
    }
}
