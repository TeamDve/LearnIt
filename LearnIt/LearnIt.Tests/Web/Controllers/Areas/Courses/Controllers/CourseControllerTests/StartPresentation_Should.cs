using LearnIt.Areas.Courses.Controllers;
using LearnIt.Data.Enums;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Courses.Controllers.CourseControllerTests
{
    public class MockHttpSession : HttpSessionStateBase
    {
        Dictionary<string, object> m_SessionStorage = new Dictionary<string, object>();

        public override object this[string name]
        {
            get { return m_SessionStorage[name]; }
            set { m_SessionStorage[name] = value; }
        }
    }

    [TestClass]
    public class StartPresentation_Should
    {
        [TestMethod]
        public void ReturnDefaultView_WhenParametersAreCorrect()
        {
            //Arrange

            string courseName = "normie";

            var mocks = new MockRepository(MockBehavior.Default);
            Mock<IPrincipal> mockPrincipal = mocks.Create<IPrincipal>();
            // create mock controller context
            var mockContext = new Mock<ControllerContext>();
            var stateBaseMock = new MockHttpSession();
            mockContext.Setup(p => p.HttpContext.Session).Returns(stateBaseMock);        
            mockContext.SetupGet(p => p.HttpContext.User).Returns(mockPrincipal.Object);
            mockContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var courseService = new Mock<ICourseService>();
            CoursesController courseController = new CoursesController(courseService.Object)
            { ControllerContext = mockContext.Object };
            


            courseService.Setup(x => x.GetCourseCompletionRate(courseName)).Returns(true);



            //Act && Assert
            courseController
               .WithCallTo(c => c.StartPresentation(courseName))
               .ShouldRenderDefaultView();
            courseService.Verify(x => x.GetCourseCompletionRate(courseName), Times.Once);

        }
    }
}
