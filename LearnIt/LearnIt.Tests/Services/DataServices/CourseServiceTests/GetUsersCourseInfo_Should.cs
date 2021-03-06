﻿using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LearnIt.Data.DataModels;
using LearnIt.Data.Enums;


namespace LearnIt.Tests.Services.DataServices.CourseServiceTests
{
    [TestClass]
    public class GetUsersCourseInfo_Should
    {
        [TestMethod]
        public void ReturnUserCourseInfo_WhenParameterIsCorrect()
        {
            //Arrange
            var dateTime = DateTime.Now;
            string userName = "test@test.test";
            var dbContextMock = new Mock<ApplicationDbContext>();
            var course = new Course()
            {
                Id = 12,
                Name = "eklerEkler",
                Description = "NeshtoSiNeshtoSi",
                DateAdded = dateTime,
                
                Required = false,
                ScoreToPass = 40
            };
              List<Course> courseList = new List<Course>()
              {
                  course
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
                    CourseId = 12,
                    Course = course,
                    AssignmentDate =dateTime,
                    CompletionDate =dateTime,
                    DueDate = dateTime,
                    Id = 1,
                    IsMandatory = true,
                    Status = CourseStatus.Pending
                   

                }
            };
            var UserCourseDbSetMock = new Mock<DbSet<UserCourse>>().SetupData(userCourseList);
            dbContextMock.SetupGet<IDbSet<UserCourse>>(x => x.UsersCourses).Returns(UserCourseDbSetMock.Object);

            List<UserCourseInfo> userCourseInfoList = new List<UserCourseInfo>()
            {
                new UserCourseInfo()
                {
                    AssignmentDate = dateTime,
                    CompletionDate = dateTime,
                    DueDate = dateTime,
                    Id = 1,
                    isMandatory = true,
                    Name = "eklerEkler",
                    Status = CourseStatus.Pending
                }
            };
     
            //Act
            CourseService courseService = new CourseService(dbContextMock.Object);
            var valueToAssertAgainst = userCourseInfoList.Single();
            var testedObject = courseService.GetUsersCourseInfo(userName).Single();
         
            //Assert
            Assert.AreEqual(valueToAssertAgainst.DueDate, testedObject.DueDate);
            Assert.AreEqual(valueToAssertAgainst.CompletionDate, testedObject.CompletionDate);
            Assert.AreEqual(valueToAssertAgainst.AssignmentDate, testedObject.AssignmentDate);
            Assert.AreEqual(valueToAssertAgainst.Name, testedObject.Name);
            Assert.AreEqual(valueToAssertAgainst.Status, testedObject.Status);
            Assert.AreEqual(valueToAssertAgainst.isMandatory, testedObject.isMandatory);
        }
    }
}
