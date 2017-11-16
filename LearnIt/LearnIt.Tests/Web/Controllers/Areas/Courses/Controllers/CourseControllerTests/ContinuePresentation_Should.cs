using LearnIt.Areas.Courses.Controllers;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Courses.Controllers.CourseControllerTests
{
    [TestClass]
    public class ContinuePresentation_Should
    {
        [TestMethod]
        public void ShouldRedirectToView()
        {
            //Arrange
            var courseServiceMock = new Mock<ICourseService>();

            var courseController = new CoursesController(courseServiceMock.Object);
            string courseName = "normie";

            //Act & Assert
            courseController
                .WithCallTo(c => c.ContinuePresentation(courseName))
                .ShouldRedirectToRoute("");


            courseServiceMock.Verify(u => u.TryCompleteCourse(courseName), Times.Once);
        }


       
    }
}
