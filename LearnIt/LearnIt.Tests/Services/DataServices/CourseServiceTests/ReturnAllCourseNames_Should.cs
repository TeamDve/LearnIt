using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LearnIt.Tests.Services.DataServices.CourseServiceTests
{
    [TestClass]
    public class ReturnAllCourseNames_Should
    {
        [TestMethod]
        public void ReturnAllCoursesInDb_WhenParametersAreCorrect()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dateTime = DateTime.Now;
            var course = new Course()
            {
                Id = 12,
                Name = "eklerEkler",
                Description = "NeshtoSiNeshtoSi",
                DateAdded = dateTime,

                Required = false,
                ScoreToPass = 40
            };
            var course1 = new Course()
            {
                Id = 12,
                Name = "palachinka",
                Description = "NeshtoSiNeshtoSi",
                DateAdded = dateTime,

                Required = false,
                ScoreToPass = 40
            };
            var course2 = new Course()
            {
                Id = 12,
                Name = "pizza",
                Description = "NeshtoSiNeshtoSi",
                DateAdded = dateTime,

                Required = false,
                ScoreToPass = 40
            };

            List<Course> courseList = new List<Course>()
            {
                course,course1,course2
            };

            var coursesMock = new Mock<DbSet<Course>>().SetupData(courseList);
            dbContextMock.SetupGet(x => x.Courses).Returns(coursesMock.Object);
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);

            var test = courseService.ReturnAllCourseNames();
            var testname = course.Name;
            var testname1=course1.Name;
            var testname2 = course2.Name;
            //Assert
            Assert.AreEqual(3,test.Count());


        }
    }
}
