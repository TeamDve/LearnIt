using LearnIt.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnIt.Areas.Courses.Models;

namespace LearnIt.Areas.Courses.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService ?? throw new ArgumentNullException("dbContext went wrong");
        }

        public ActionResult AllCourses()
        {
            var viewModel = courseService.GetAllCourses()
                                .Select(x => new AllCourseInfo()
                                {
                                    Name = x.Name,
                                    DateAdded = x.DateAdded,
                                    Description = x.Description,
                                    ScoreToPass = x.ScoreToPass
                                });
            return this.View(viewModel);
        }

        public ActionResult MyCourses()
        {
            var viewModel = courseService.GetUsersCourseInfo(this.User.Identity.Name)
                                        .Select(x => new MyCourseInfo()
                                        {
                                            Name = x.Name,
                                            DueDate = x.DueDate,
                                            IsMandatory = x.isMandatory
                                        });
            return this.View(viewModel);
        }

        [ActionName("GoStudy")]
        public ActionResult StartPresentation(string name)
        {
            ViewBag.smth = courseService.GetCourseCompletionRate(name);
            return this.View();
        }

        [ChildActionOnly]
        public ActionResult Questions(string name)
        {
            IEnumerable<QuestionInfo> questionsAndAnswers = courseService.GetAllCourseQuestions(name)
                                                               .Select(x => new QuestionInfo()
                                                               {
                                                                   Qstn = x.Qstn,
                                                                   Answers = (x.Answers).Split('/'),
                                                                   RightAnswer = x.RightAnswer
                                                               });
            return this.PartialView("_Questions",questionsAndAnswers);
        }

        [ChildActionOnly]
        public ActionResult Slides(string name)
        {
            IEnumerable<SlideImage> slideImages = courseService.GetAllCourseSlides(name)
                                                    .Select(x => new SlideImage()
                                                    {
                                                        Order = x.Order,
                                                        ImageBase64 = Convert.ToBase64String(x.ImageBinary)
                                                    });
            return this.PartialView("_Slides", slideImages);
        }

    }
}