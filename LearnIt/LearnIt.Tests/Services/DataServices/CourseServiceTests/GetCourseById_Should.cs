using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LearnIt.Tests.Services.DataServices.CourseServiceTests
{
    [TestClass]
    public class GetCourseById_Should
    {
        [TestMethod]
        public void ReturnCourse_WhenCourseIdIsCorrect()
        {
            //Arrange
            var dateTime = DateTime.Now;
            int idConst = 12;
            var dbContextMock = new Mock<ApplicationDbContext>();
            List<Course> courseList = new List<Course>()
            {
                new Course()
                {
                    Id = idConst,
                    Name = "eklerEkler",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                }
            };
            var courseDbSetMock = new Mock<DbSet<Course>>().SetupData(courseList);
            dbContextMock.SetupGet<IDbSet<Course>>(x=>x.Courses).Returns(courseDbSetMock.Object);
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            //Assert
            Assert.AreEqual(courseList.First(),courseService.GetCourseById(idConst));
        }

        [TestMethod]
        public void ReturnEmptyCourse_WhenNoCourseIsFound()
        {
            //Arrange
            var dateTime = DateTime.Now;
            int idConst = 12;
            var dbContextMock = new Mock<ApplicationDbContext>();
            List<Course> courseList = new List<Course>()
            {

            };
            var courseDbSetMock = new Mock<DbSet<Course>>().SetupData(courseList);
            dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(courseDbSetMock.Object);
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            //Assert
            Assert.IsNull(courseService.GetCourseById(idConst));
        }
    }
}
