using LearnIt.Areas.Courses.Controllers;
using LearnIt.Areas.Courses.Models;
using LearnIt.Data.DataModels;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Courses.Controllers.CourseControllerTests
{
    [TestClass]
    public class MyCourses_Should
    {
        [TestMethod]
        public void ReturnDefaultView_WhenParametersAreCorrect()
        {
            //Arrange

            // create mock principal
            string userName = "normie";
            var mocks = new MockRepository(MockBehavior.Default);
            Mock<IPrincipal> mockPrincipal = mocks.Create<IPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.Name).Returns(userName);
            mockPrincipal.Setup(p => p.IsInRole("User")).Returns(true);

            // create mock controller context
            var mockContext = new Mock<ControllerContext>();
            mockContext.SetupGet(p => p.HttpContext.User).Returns(mockPrincipal.Object);
            mockContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var courseServiceMock = new Mock<ICourseService>();
            CoursesController courseController = new CoursesController(courseServiceMock.Object) { ControllerContext = mockContext.Object };
            //var resultViewModel = users.AsQueryable().Select(UserViewModel.Create).ToList()
            List<MyCourseInfo> resultViewModel = new List<MyCourseInfo>()
            {
                new MyCourseInfo()
                {
                    Name = "Knitting for dummies"
                },
                new MyCourseInfo()
                {
                    Name = "Normies"
                }
            };

            

            courseServiceMock.Setup(x => x.GetUsersCourseInfo(It.IsAny<string>())).Returns(resultViewModel.Select(x => new UserCourseInfo() { Name = x.Name }));
            //courseController.MyCourses();
           
            //Act && Assert
            courseController
                .WithCallTo(c => c.MyCourses())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<MyCourseInfo>>(viewModel =>
                {
                    int cnt = 0;
                    foreach (var item in viewModel)
                    {
                        Assert.AreEqual(resultViewModel[cnt].Name, item.Name);
                        cnt++;
                    }
                });

            courseServiceMock.Verify(x => x.GetUsersCourseInfo(It.IsAny<string>()), Times.Once);
        }

    }
}
