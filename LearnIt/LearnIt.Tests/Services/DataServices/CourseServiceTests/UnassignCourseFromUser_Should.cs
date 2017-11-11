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
    public class UnassignCourseFromUser_Should
    {
        [TestMethod]
        public async Task RemoveCourseFrom_WhenParametersAreCorrect()
        {
            //Arrange
            int courseId = 1;
            string username = "testUser";
            string userId = "guidStandIn";
            DateTime date = DateTime.Now;
            int status = 1;
            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = username
            };
            var course = new Course()
            {
                Id = courseId,
                Name = "1",
                Description = "NeshtoSiNeshtoSi",
                DateAdded = date,
                Required = true,
                ScoreToPass = 40
            };

            var userCourse = new UserCourse()
            {
                CourseId = courseId,
                UserId = userId
            };

            var dbContextMock = new Mock<ApplicationDbContext>();
            List<UserCourse> usersCoursesList = new List<UserCourse>() { userCourse };
            var usersCoursesDbSetMock = new Mock<DbSet<UserCourse>>().SetupData(usersCoursesList);
            dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(usersCoursesDbSetMock.Object);

            List<ApplicationUser> usersList = new List<ApplicationUser>() { user };
            var usersDbSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(usersList);
            dbContextMock.SetupGet<IDbSet<ApplicationUser>>(x => x.Users).Returns(usersDbSetMock.Object);

            List<Course> coursesList = new List<Course>() { course };
            var coursesDbSetMock = new Mock<DbSet<Course>>().SetupData(coursesList);
            dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(coursesDbSetMock.Object);

            CourseService courseService = new CourseService(dbContextMock.Object);
            //Act
            await courseService.UnassignCourseFromUser(courseId, username);
            //Assert
            Assert.AreEqual(0, dbContextMock.Object.UsersCourses.Count<UserCourse>());
        }

        [TestMethod]
        public async Task ThrowException_WhenNoCoursesAreFound()
        {
            //Arrange
            int courseId = 1;
            string username = "testUser";
            string userId = "guidStandIn";
            DateTime date = DateTime.Now;
            int status = 1;
            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = username
            };
            var course = new Course()
            {
                Id = 5,
                Name = "1",
                Description = "NeshtoSiNeshtoSi",
                DateAdded = date,
                Required = true,
                ScoreToPass = 40
            };

            var userCourse = new UserCourse()
            {
                CourseId = courseId,
                UserId = userId
            };

            var dbContextMock = new Mock<ApplicationDbContext>();
            List<UserCourse> usersCoursesList = new List<UserCourse>() { userCourse };
            var usersCoursesDbSetMock = new Mock<DbSet<UserCourse>>().SetupData(usersCoursesList);
            dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(usersCoursesDbSetMock.Object);

            List<ApplicationUser> usersList = new List<ApplicationUser>() { user };
            var usersDbSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(usersList);
            dbContextMock.SetupGet<IDbSet<ApplicationUser>>(x => x.Users).Returns(usersDbSetMock.Object);

            List<Course> coursesList = new List<Course>() { course };
            var coursesDbSetMock = new Mock<DbSet<Course>>().SetupData(coursesList);
            dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(coursesDbSetMock.Object);

            CourseService courseService = new CourseService(dbContextMock.Object);
            //Act && Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => courseService.UnassignCourseFromUser(courseId, username));
        }
    }
}
