using LearnIt.Areas.Courses.Controllers;
using LearnIt.Areas.Courses.Models;
using LearnIt.Data.Context;
using LearnIt.Data.DataModels;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Courses.Controllers.CourseControllerTests
{
    [TestClass]
    public class AllCourses_Should
    {
        [TestMethod]
        public void ReturnDefaultView_WhenParametersAreCorrect()
        {   //Arrange
            var courseService = new Mock<ICourseService>();
            CoursesController courseController = new CoursesController(courseService.Object);
            //var resultViewModel = users.AsQueryable().Select(UserViewModel.Create).ToList()
            List<AllCourseInfo> resultViewModel = new List<AllCourseInfo>()
            {
                new AllCourseInfo()
                {
                    Name = "Evdokiq"
                },
                new AllCourseInfo()
                {
                    Name = "Haralampii"
                }
            };
            courseService.Setup(x => x.GetAllCourses()).Returns(resultViewModel.Select(x => new CourseInfoData() { Name = x.Name }).ToList());
            courseController.AllCourses();

            //Act && Assert
            courseService.Verify(x => x.GetAllCourses(), Times.Once);

            courseController
                .WithCallTo(c => c.AllCourses())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<AllCourseInfo>>(viewModel =>
                {
                    int cnt = 0;
                    foreach(var item in viewModel)
                    {
                        Assert.AreEqual(resultViewModel[cnt].Name, item.Name);
                        cnt++;
                    }
                });
        }
    }
}
