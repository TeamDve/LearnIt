using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LearnIt.Tests.Services.DataServices.CourseServiceTests
{
    [TestClass]
    public class AddCoursesToDb_Should
    {
        [TestMethod]
        public async Task AddCourseToTheDb_WhenParametersAreCorrect()
        {
            //Arrange
            string name = "testCourse";
            string desc = "just another testCourse for me to test the method";
            DateTime date = DateTime.Now;
            int scoreToPass = 55;
            bool required = true;
            var dbContextMock = new Mock<ApplicationDbContext>();
            List<Course> coursesList = new List<Course>();
            var courseDbSetMock = new Mock<DbSet<Course>>().SetupData(coursesList);
            dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(courseDbSetMock.Object);
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            await courseService.AddCourseToDb(name, desc, date, scoreToPass, required);
            var ActualResult = dbContextMock.Object.Courses.First();
            //Assert
            Assert.AreEqual(name, ActualResult.Name);
            Assert.AreEqual(desc, ActualResult.Description);
            Assert.AreEqual(date, ActualResult.DateAdded);
            Assert.AreEqual(scoreToPass, ActualResult.ScoreToPass);
            Assert.AreEqual(required, ActualResult.Required);
            dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task AddCourseToTheDb_WhenCourseProvidedIsCorrect()
        {
            //Arrange
            var course = new Course()
            {
                Id = 12,
                Name = "1",
                Description = "NeshtoSiNeshtoSi",
                DateAdded = DateTime.Now,
                Required = true,
                ScoreToPass = 40
            };

            var dbContextMock = new Mock<ApplicationDbContext>();
            List<Course> coursesList = new List<Course>();
            var courseDbSetMock = new Mock<DbSet<Course>>().SetupData(coursesList);
            dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(courseDbSetMock.Object);
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            await courseService.AddCourseToDb(course);
            var ActualResult = dbContextMock.Object.Courses.First();
            //Assert
            Assert.AreEqual(course, ActualResult);
            dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}

