using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

//<<<<<<< HEAD
namespace LearnIt.Tests.Services.DataServices.CourseServiceTests
{
    [TestClass]
    public class GetAllCourses_Should
    {
        [TestMethod]
        public void ReturnListOfCourses_WhenTheyArePresent()
        {
            //Arrange
            var dateTime = DateTime.Now;
            var dbContextMock = new Mock<ApplicationDbContext>();
            List<Course> courseList = new List<Course>()
            {
                new Course()
                {
                    Name = "eklerEkler",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                }
            };
            var coursesMock = new Mock<DbSet<Course>>().SetupData(courseList);
            dbContextMock.SetupGet(x => x.Courses).Returns(coursesMock.Object);
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            var valueToAssertAgainst = courseList.Single();
            var testedObject = courseService.GetAllCourses().Single();
            //Assert
            Assert.AreEqual(valueToAssertAgainst.DateAdded, testedObject.DateAdded);
            Assert.AreEqual(valueToAssertAgainst.Description, testedObject.Description);
            Assert.AreEqual(valueToAssertAgainst.Name, testedObject.Name);
            Assert.AreEqual(valueToAssertAgainst.ScoreToPass, testedObject.ScoreToPass);
        }
        [TestMethod]
        public void ReturnEmptyListOfCourses_WhenNoneArePresent()
        {
            //Arrange
            var dateTime = DateTime.Now;
            var dbContextMock = new Mock<ApplicationDbContext>();
            List<Course> courseList = new List<Course>()
            {
            };
            var coursesMock = new Mock<DbSet<Course>>().SetupData(courseList);
            dbContextMock.SetupGet(x => x.Courses).Returns(coursesMock.Object);
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            //Assert
            Assert.AreNotEqual(courseList, courseService.GetAllCourses());
        }
    }
}
