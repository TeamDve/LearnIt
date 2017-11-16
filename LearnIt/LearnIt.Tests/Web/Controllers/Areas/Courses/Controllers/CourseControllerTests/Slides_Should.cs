using LearnIt.Areas.Courses.Controllers;
using LearnIt.Areas.Courses.Models;
using LearnIt.Data.DataModels;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Courses.Controllers.CourseControllerTests
{
    [TestClass]
    public class Slides_Should
    {
        [TestMethod]
        public void RenderPartialView_WhenExecuted()
        {
            //Arrange
            byte[] imageBinary = { 1, 2, 4, 8, 16, 32 };
            string courseName = "normie";
            var courseServiceMock = new Mock<ICourseService>();
            List<CourseSlidesBinary> courseSlidesList = new List<CourseSlidesBinary>()
            {
               new CourseSlidesBinary()
               {
                   ImageBinary = imageBinary,
                   Order = 5
               }
            };
            courseServiceMock.Setup(x => x.GetAllCourseSlides(courseName)).Returns(courseSlidesList);
            CoursesController courseController = new CoursesController(courseServiceMock.Object);
            //Act && Assert
            courseController
            .WithCallTo(c => c.Slides(courseName))
            .ShouldRenderPartialView("_Slides")
            .WithModel<IEnumerable<SlideImage>>(QList =>
            {
                int cnt = 0;
                foreach (var item in QList)
                {
                    Assert.AreEqual(courseSlidesList[cnt].Order, item.Order);
                    cnt++;
                }

            });
            courseServiceMock.Verify(x => x.GetAllCourseSlides(courseName), Times.Once);
        }
    }
}
