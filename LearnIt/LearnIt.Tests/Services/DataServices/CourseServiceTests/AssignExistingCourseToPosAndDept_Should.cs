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
    public class AssignExistingCourseToPosAndDept_Should
    {

        //[TestMethod]
        //public async Task AddCourseToPosAndDept_WhenParametersAreCorrect()
        //{
        //    //Arrange
        //    int courseId = 1;
        //    int deptId = 1;
        //    int posId = 1;
        //    DateTime date = DateTime.Now;
        //    int status = 1;

        //    var dbContextMock = new Mock<ApplicationDbContext>();
        //    List<UserCourse> usersCoursesList = new List<UserCourse>() { };
        //    var usersCoursesDbSetMock = new Mock<DbSet<UserCourse>>().SetupData(usersCoursesList);
        //    dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(usersCoursesDbSetMock.Object);

        //    List<ApplicationUser> usersList = new List<ApplicationUser>()
        //    {
        //        new ApplicationUser()
        //            {
        //                Id = "uniqueGuidOne",
        //                UserName = "first",
        //                DepartmentId = 1,
        //                PositionId = 1
        //            },
        //        new ApplicationUser()
        //            {
        //                Id = "uniqueGuidTwo",
        //                UserName = "second",
        //                DepartmentId = 1,
        //                PositionId = 1
        //            },
        //        new ApplicationUser()
        //            {
        //                Id = "uniqueGuidThree",
        //                UserName = "third",
        //                DepartmentId = 1,
        //                PositionId = 2
        //            }
        //    };
        //    var usersDbSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(usersList);
        //    dbContextMock.SetupGet<IDbSet<ApplicationUser>>(x => x.Users).Returns(usersDbSetMock.Object);

        //    CourseService courseService = new CourseService(dbContextMock.Object);
        //    //Act
        //    await courseService.AssignExistingCourseToPosAndDept(courseId, deptId, posId, date, status);
        //    //Assert
        //    Assert.AreEqual(2, dbContextMock.Object.UsersCourses.Count<UserCourse>());
        //    dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        //}

        //[TestMethod]
        //public async Task VerifyDbQueryExecution_WhenParametersAreIncorrect()
        //{
        //    //Arrange
        //    int courseId = 1;
        //    int deptId = 1;
        //    int posId = 1;
        //    DateTime date = DateTime.Now;
        //    int status = 1;

        //    var dbContextMock = new Mock<ApplicationDbContext>();
        //    List<UserCourse> usersCoursesList = new List<UserCourse>() { };
        //    var usersCoursesDbSetMock = new Mock<DbSet<UserCourse>>().SetupData(usersCoursesList);
        //    dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(usersCoursesDbSetMock.Object);

        //    List<ApplicationUser> usersList = new List<ApplicationUser>()
        //    {
        //        new ApplicationUser()
        //            {
        //                Id = "uniqueGuidOne",
        //                UserName = "first",
        //                DepartmentId = 1,
        //                PositionId = 2
        //            },
        //        new ApplicationUser()
        //            {
        //                Id = "uniqueGuidTwo",
        //                UserName = "second",
        //                DepartmentId = 1,
        //                PositionId = 3
        //            },
        //        new ApplicationUser()
        //            {
        //                Id = "uniqueGuidThree",
        //                UserName = "third",
        //                DepartmentId = 1,
        //                PositionId = 2
        //            }
        //    };
        //    var usersDbSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(usersList);
        //    dbContextMock.SetupGet<IDbSet<ApplicationUser>>(x => x.Users).Returns(usersDbSetMock.Object);

        //    CourseService courseService = new CourseService(dbContextMock.Object);
        //    //Act
        //    await courseService.AssignExistingCourseToPosAndDept(courseId, deptId, posId, date, status);
        //    //Assert
        //    Assert.AreEqual(0, dbContextMock.Object.UsersCourses.Count<UserCourse>());
        //    dbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        //}

    }
}
