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
    public class GetUserCourses_Should
    {
        [TestMethod]
        public void ReturnListOfCourses_WhenParameterIsCorrect()
        {
            //Arrange
            var dateTime = DateTime.Now;
            string userName = "test@test.test";
            var dbContextMock = new Mock<ApplicationDbContext>();

            List<Course> courseList = new List<Course>()
            {
                new Course()
                {
                    Id = 12,
                    Name = "eklerEkler",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                }
            };
            var coursesMock = new Mock<DbSet<Course>>().SetupData(courseList);
            dbContextMock.SetupGet(x => x.Courses).Returns(coursesMock.Object);

            List<ApplicationUser> userList = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName=userName,
                    Id = "asd"
                }
            };
            var usersDbSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(userList);
            dbContextMock.SetupGet<IDbSet<ApplicationUser>>(x => x.Users).Returns(usersDbSetMock.Object);

            List<UserCourse> userCourseList = new List<UserCourse>()
            {
                new UserCourse()
                {
                    UserId = "asd",
                    CourseId = 12                }
            };
            var UserCourseDbSetMock = new Mock<DbSet<UserCourse>>().SetupData(userCourseList);
            dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(UserCourseDbSetMock.Object);

            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            var valueToAssertAgainst = courseList.Single();
            var testedObject = courseService.GetUserCourses(userName).Single();
            //Assert
            Assert.AreEqual(valueToAssertAgainst.DateAdded, testedObject.DateAdded);
            Assert.AreEqual(valueToAssertAgainst.Description, testedObject.Description);
            Assert.AreEqual(valueToAssertAgainst.Name, testedObject.Name);
            Assert.AreEqual(valueToAssertAgainst.Required, testedObject.Required);
            Assert.AreEqual(valueToAssertAgainst.ScoreToPass, testedObject.ScoreToPass);
        }

        [TestMethod]
        public void ThrowException_WhenTheUsernameIsNotPresentInTheDb()
        {
            //Arrange
            var dateTime = DateTime.Now;
            var dbContextMock = new Mock<ApplicationDbContext>();
            List<Course> courseList = new List<Course>()
            {
                new Course()
                {
                    Id = 12,
                    Name = "eklerEkler",
                    Description = "NeshtoSiNeshtoSi",
                    DateAdded = dateTime,
                    Required = true,
                    ScoreToPass = 40
                }
            };
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => courseService.GetAllCourses());
        }
    }
}
