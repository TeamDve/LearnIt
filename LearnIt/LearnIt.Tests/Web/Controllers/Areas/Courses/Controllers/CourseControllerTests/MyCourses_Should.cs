//using LearnIt.Areas.Courses.Controllers;
//using LearnIt.Areas.Courses.Models;
//using LearnIt.Data.DataModels;
//using LearnIt.Data.Services.Contracts;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Principal;
//using System.Web;
//using System.Web.Mvc;
//using TestStack.FluentMVCTesting;

//namespace LearnIt.Tests.Web.Controllers.Areas.Courses.Controllers.CourseControllerTests
//{
//    [TestClass]
//    public class MyCourses_Should
//    {
//        [TestMethod]
//        public void ReturnDefaultView_WhenParametersAreCorrect()
//        {
//            //Arrange
//            var courseService = new Mock<ICourseService>();
//            CoursesController courseController = new CoursesController(courseService.Object);
//            //var resultViewModel = users.AsQueryable().Select(UserViewModel.Create).ToList()
//            List<MyCourseInfo> resultViewModel = new List<MyCourseInfo>()
//            {
//                new MyCourseInfo()
//                {
//                    Name = "Knitting for dummies"
//                },
//                new MyCourseInfo()
//                {
//                    Name = "Knitting for dummies"
//                }
//            };

//            var fakeHttpContext = new Mock<HttpContextBase>();
//            var fakeIdentity = new GenericIdentity("User");
//            var principal = new GenericPrincipal(fakeIdentity, null);

//            fakeHttpContext.Setup(t => t.User).Returns(principal);
//            var controllerContext = new Mock<ControllerContext>();
//            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

//            courseService.Setup(x => x.GetUsersCourseInfo(It.IsAny<string>())).Returns(resultViewModel.Select(x => new UserCourseInfo() { Name = x.Name }));
//            courseController.MyCourses();
//            courseController.ControllerContext = controllerContext.Object;
//            //Act && Assert
//            courseService.Verify(x => x.GetUsersCourseInfo(It.IsAny<string>()), Times.Once);

//            //courseController
//            //    .WithCallTo(c => c.MyCourses())
//            //    .ShouldRenderDefaultView()
//            //    .WithModel<IEnumerable<UserCourseInfo>>(viewModel =>
//            //    {
//            //        int cnt = 0;
//            //        foreach (var item in viewModel)
//            //        {
//            //            Assert.AreEqual(resultViewModel[cnt].Name, item.Name);
//            //            cnt++;
//            //        }
//            //    });
//        }

//    }
//}
