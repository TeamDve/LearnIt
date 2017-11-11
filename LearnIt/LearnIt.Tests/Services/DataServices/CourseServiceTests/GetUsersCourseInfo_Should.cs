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
    public class GetUsersCourseInfo_Should
    {
        //To Be continued, smth wrong with the method itself
        [TestMethod]
        public void ReturnUserCourseInfo_WhenParameterIsCorrect()
        {
            
            //var dbContextMock = new Mock<ApplicationDbContext>();

            //List<Course> courseList = new List<Course>()
            //{
            //    new Course()
            //    {
            //        Id = 1,
            //        Name = "testCourse",
            //        Required = true,
            //        ScoreToPass = 55
            //    }
            //};
            //var coursesDbSetMock = new Mock<DbSet<Course>>().SetupData<Course>(courseList);
            //dbContextMock.SetupGet<IDbSet<Course>>(x => x.Courses).Returns(coursesDbSetMock.Object);

            //List<UserCourse> userCourseList = new List<UserCourse>()
            //{

            //};
            //var userCoursesDbSetMock = new Mock<DbSet<UserCourse>>().SetupData<UserCourse>(userCourseList);
            //dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(userCoursesDbSetMock.Object);

        }
    }
}
