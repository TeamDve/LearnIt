using LearnIt.Areas.Admin.Controllers;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace LearnIt.Tests.Web.Controllers.Areas.Admin.Contrellers.AdminControllerTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void ReturnInstanceOfAdminController_Correct()
        {
            // Arrange
            var jsonParserMock = new Mock<IJsonParserService>();
            var courseServiceMock = new Mock<ICourseService>();
            var userServicesMock = new Mock<IUserServices>();
            var departmentServiceMock = new Mock<IDepartmenService>();
            var possitionServiceMock = new Mock<IPositionService>();

            // Act
            var adminController = new AdminController(
                jsonParserMock.Object,
                courseServiceMock.Object,
                userServicesMock.Object,
                departmentServiceMock.Object,
                possitionServiceMock.Object);

            // Assert
            Assert.IsNotNull(adminController);
            Assert.IsInstanceOfType(adminController, typeof(AdminController));
        }

        [TestMethod]
        public void ThrowWhen_IJsonParserServiceIsNull()
        {
            // Arrange
            var courseServiceMock = new Mock<ICourseService>();
            var userServicesMock = new Mock<IUserServices>();
            var departmentServiceMock = new Mock<IDepartmenService>();
            var possitionServiceMock = new Mock<IPositionService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminController(
                null,
                courseServiceMock.Object,
                userServicesMock.Object,
                departmentServiceMock.Object,
                possitionServiceMock.Object), "jsonParserMock");


        }

        [TestMethod]
        public void ThrowWhen_CourseServiceIsNull()
        {
            // Arrange
            var jsonParserMock = new Mock<IJsonParserService>();
            var userServicesMock = new Mock<IUserServices>();
            var departmentServiceMock = new Mock<IDepartmenService>();
            var possitionServiceMock = new Mock<IPositionService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminController(
                jsonParserMock.Object,
                null,
                userServicesMock.Object,
                departmentServiceMock.Object,
                possitionServiceMock.Object), "courseServiceMock");
        }

        [TestMethod]
        public void ThrowWhen_UserServicesIsNull()
        {
            // Arrange
            var jsonParserMock = new Mock<IJsonParserService>();
            var courseServiceMock = new Mock<ICourseService>();
            var userServicesMock = new Mock<IUserServices>();
            var departmentServiceMock = new Mock<IDepartmenService>();
            var possitionServiceMock = new Mock<IPositionService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminController(
                jsonParserMock.Object,
                courseServiceMock.Object,
                null,
                departmentServiceMock.Object,
                possitionServiceMock.Object), "userServicesMock");
        }

        [TestMethod]
        public void ThrowWhen_DepartmentServiceIsNull()
        {
            // Arrange
            var jsonParserMock = new Mock<IJsonParserService>();
            var courseServiceMock = new Mock<ICourseService>();
            var userServicesMock = new Mock<IUserServices>();
            var possitionServiceMock = new Mock<IPositionService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminController(
                jsonParserMock.Object,
                courseServiceMock.Object,
                userServicesMock.Object,
                null,
                possitionServiceMock.Object), "departmentServiceMock");
        }

        [TestMethod]
        public void ThrowWhen_PossitionServiceIsNull()
        {
            // Arrange
            var jsonParserMock = new Mock<IJsonParserService>();
            var courseServiceMock = new Mock<ICourseService>();
            var userServicesMock = new Mock<IUserServices>();
            var departmentServiceMock = new Mock<IDepartmenService>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new AdminController(
                jsonParserMock.Object,
                courseServiceMock.Object,
                userServicesMock.Object,
                departmentServiceMock.Object,
                null), "possitionServiceMock");
        }
    }
}
