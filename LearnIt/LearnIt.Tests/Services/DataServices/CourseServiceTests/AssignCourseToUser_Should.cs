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
    public class AssignCourseToUser_Should
    {
        [TestMethod]
        public async Task AddCourseToUser_WhenParametersAreCorrect()
        {
            //Arrange
            string coureseName = "CourseName";
            string username = "testUser";
            DateTime date = DateTime.Now;
            int status = 1;
            var user = new ApplicationUser()
            {
                Id = "guidStandIn",
                UserName = username
            };
            var course = new Course()
            {
                Id = 1,
                Name = "1",
                Description = "NeshtoSiNeshtoSi",
                DateAdded = date,
                Required = true,
                ScoreToPass = 40
            };

            var dbContextMock = new Mock<ApplicationDbContext>();
            List<UserCourse> usersCoursesList = new List<UserCourse>();

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
            await courseService.AssignCourseToUser(course.Name, username, date, course.Required);
            //Assert
            Assert.AreEqual(1, dbContextMock.Object.UsersCourses.Count<UserCourse>());
            dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [TestMethod]
        public async Task ThrowException_WhenCourseDoesNotExist()
        {
            //Arrange
            string courseId = "1";
            string username = "testUser";
            DateTime date = DateTime.Now;
            int status = 1;
            var user = new ApplicationUser()
            {
                Id = "guidStandIn",
                UserName = username
            };

            var dbContextMock = new Mock<ApplicationDbContext>();
            List<UserCourse> usersCoursesList = new List<UserCourse>();
            var usersCoursesDbSetMock = new Mock<DbSet<UserCourse>>().SetupData(usersCoursesList);
            dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(usersCoursesDbSetMock.Object);

            List<ApplicationUser> usersList = new List<ApplicationUser>() { user };
            var usersDbSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(usersList);
            dbContextMock.SetupGet<IDbSet<ApplicationUser>>(x => x.Users).Returns(usersDbSetMock.Object);

            List<Course> coursesList = new List<Course>() { };
            var coursesDbSetMock = new Mock<DbSet<Course>>().SetupData(coursesList);
            dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(coursesDbSetMock.Object);

            CourseService courseService = new CourseService(dbContextMock.Object);
            //Act && Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => courseService.AssignCourseToUser(courseId, username, date, true));
        }
    }
}
