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
    public class GetLast_Should
    {
        [TestMethod]
        public void ReturnLastFiveAddedCoursesOrderedByDateAdded_WhenThereAreFiveCoursesInTheDb()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dateTime = DateTime.Now;
            var howMany = 5;
            List<Course> coursesList = new List<Course>()
            {
            new Course()
                {
                    Id = 12,
                    Name = "1",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                },
            new Course()
                {
                    Id = 12,
                    Name = "2",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                },
            new Course()
                {
                    Id = 12,
                    Name = "3",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                },
            new Course()
                {
                    Id = 12,
                    Name = "4",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                },
            new Course()
                {
                    Id = 12,
                    Name = "5",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                },
            new Course()
                {
                    Id = 12,
                    Name = "6",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime.AddHours(5),
                    Required = true,
                    ScoreToPass = 40
                }
            };
            var coursesDbSetMock = new Mock<DbSet<Course>>().SetupData(coursesList);
            dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(coursesDbSetMock.Object);
            CourseService courseService = new CourseService(dbContextMock.Object);
            //Act
            var expectedResult = courseService.GetLast(howMany);
            string ExpectedOrder = string.Join(" ", coursesList.OrderByDescending(x => x.DateAdded).Select(x => x.Name).Take(5));
            string ActualOrder = string.Join(" ", expectedResult.Select(x=>x.Name));
            //Assert
            Assert.AreEqual(howMany, expectedResult.Count<Course>());
            Assert.AreEqual(ExpectedOrder, ActualOrder);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void ReturnCourses_WhenBetweenZeroAndFiveCoursesInTheDb(int howMany)
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var dateTime = DateTime.Now;
            List<Course> coursesList = new List<Course>()
            {
            };
            for (int i = 0; i < howMany; i++)
            {
                coursesList.Add(new Course());
            }
            var coursesDbSetMock = new Mock<DbSet<Course>>().SetupData(coursesList);
            dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(coursesDbSetMock.Object);
            CourseService courseService = new CourseService(dbContextMock.Object);
            //Act
            var expectedResult = courseService.GetLast(howMany);
            //Assert
            Assert.AreEqual(howMany, expectedResult.Count<Course>());
        }
    }
}
