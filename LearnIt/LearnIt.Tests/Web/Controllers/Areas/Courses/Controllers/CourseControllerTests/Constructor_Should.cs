using LearnIt.Areas.Courses.Controllers;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIt.Tests.Web.Controllers.Areas.Courses.Controllers.CourseControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void InitializeController_WhenProperArgumentsAreGiven()
        {
            //Arrange
            var courseService = new Mock<ICourseService>();
            //Act
            var controller = new CoursesController(courseService.Object);
            //Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void ThrowException_WhenWrongArgumentsAreGive()
        {
            //Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CoursesController(null));
        }
    }
}
