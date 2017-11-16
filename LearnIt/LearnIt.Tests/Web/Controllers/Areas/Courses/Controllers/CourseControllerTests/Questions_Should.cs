using LearnIt.Areas.Courses.Controllers;
using LearnIt.Areas.Courses.Models;
using LearnIt.Data.DataModels;
using LearnIt.Data.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace LearnIt.Tests.Web.Controllers.Areas.Courses.Controllers.CourseControllerTests
{
    [TestClass]
    public class Questions_Should
    {
        [TestMethod]
        public void RenderPartialView_WhenExecuted()
        {
            //Arrange
            var qstn = "what";
            var answers = "test/test1/test2";
            var rightAnswer = "test";
            var courseName = "normie";
            var courseServiceMock = new Mock<ICourseService>();
            List<CourseQuestions> courseQuestionsList = new List<CourseQuestions>()
            {
                new CourseQuestions()
                {
                    Qstn = qstn,
                    Answers = answers,
                    RightAnswer = rightAnswer
                }
            };
            List<QuestionInfo> resultQuestionList = new List<QuestionInfo>()
            {
                new QuestionInfo()
                {
                    Qstn=qstn,
                    Answers = answers.Split('/'),
                    RightAnswer = rightAnswer
                }
            };
            courseServiceMock.Setup(x => x.GetAllCourseQuestions(courseName)).Returns(courseQuestionsList);
            CoursesController courseController = new CoursesController(courseServiceMock.Object);
            //Act && Assert
            courseController
            .WithCallTo(c => c.Questions(courseName))
            .ShouldRenderPartialView("_Questions")
            .WithModel<List<QuestionInfo>>(QList =>
            {
                for (int i = 0; i < QList.Count; i++)
                {
                    Assert.AreEqual(courseQuestionsList[i].Qstn, QList[i].Qstn);
                }
            });
            courseServiceMock.Verify(x => x.GetAllCourseQuestions(courseName), Times.Once);
        }
    }
}
