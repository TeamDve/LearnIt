﻿using LearnIt.Areas.Admin.Controllers;
using LearnIt.Areas.Admin.Models;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Admin.Contrellers.AdminControllerTests
{
    [TestClass]
    public class SinglePersonCourseAssignShould
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

            var singleCourseAsignModelMock = new CourseToUser
            {
                Username = "fake@user.com",
                CourseName = "SomeCourse",
                DueDate = DateTime.Now,
                IsMandatory = true
            };

            //Act & Assert
            adminContoller
                .WithCallTo(c => c.SinglePersonCourseAssign(singleCourseAsignModelMock))
                .ShouldRedirectToRoute("");

            courseServiceMock.Verify(c=>c.AssignCourseToUser(
                singleCourseAsignModelMock.CourseName,
                singleCourseAsignModelMock.Username,
                singleCourseAsignModelMock.DueDate,
                singleCourseAsignModelMock.IsMandatory),Times.Once);

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
                .WithCallTo(c => c.SinglePersonCourseAssign())
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

            var singleCourseAsignModelMock = new CourseToUser
            {
                Username = "fake@user.com",
                CourseName = "SomeCourse",
                DueDate = DateTime.Now,
                IsMandatory = true
            };

            courseServiceMock.Setup(c => c.AssignCourseToUser(
                singleCourseAsignModelMock.CourseName,
                singleCourseAsignModelMock.Username,
                singleCourseAsignModelMock.DueDate,
                singleCourseAsignModelMock.IsMandatory))
                .Throws<ArgumentNullException>();
            //Act & Assert
            adminContoller
                .WithCallTo(c => c.SinglePersonCourseAssign(singleCourseAsignModelMock))
                .ShouldRenderDefaultView().WithModel<CourseToUser>();

            courseServiceMock.Verify(c => c.ReturnAllCourseNames(), Times.Once);
            userServicesMock.Verify(u => u.ReturnAllUserNames(), Times.Once);
        }
    }
}
