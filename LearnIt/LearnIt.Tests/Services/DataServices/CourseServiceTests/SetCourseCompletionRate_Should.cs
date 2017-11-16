using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnIt.Data.Context;
using LearnIt.Data.Enums;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LearnIt.Tests.Services.DataServices.CourseServiceTests
{
    [TestClass]
    public class SetCourseCompletionRate_Should
    {
        [TestMethod]
        public void ChangeValueOfProperty_WhenParametersAreCorrect()
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
                    CourseId = 12      ,
                    Status = CourseStatus.Completed
                }
            };
            var UserCourseDbSetMock = new Mock<DbSet<UserCourse>>().SetupData(userCourseList);
            dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(UserCourseDbSetMock.Object);
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            courseService.SetCourseCompletionRate("eklerEkler", true);
            var valueToAssertAgainst = userCourseList.Single();

            //Assert
            Assert.AreEqual(CourseStatus.Completed, valueToAssertAgainst.Status);

        }
    }
}
