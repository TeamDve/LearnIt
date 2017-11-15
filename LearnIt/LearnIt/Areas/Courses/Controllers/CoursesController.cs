using LearnIt.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnIt.Areas.Courses.Models;
using System.Threading.Tasks;

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

        [HttpGet]
        [ActionName("GoStudy")]
        public ActionResult StartPresentation(string name)
        {
            ViewBag.areQuestionsAvailable = courseService.GetCourseCompletionRate(name);
            ViewBag.HiddenInput = name;
            return this.View();
        }

        [HttpPost]
        [ActionName("GoStudy")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ContinuePresentation(string courseName)
        {
            await courseService.TryCompleteCourse(courseName);
            return RedirectToAction("GoStudy","Courses", new { name = courseName });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendAnswers(IEnumerable<QuestionInfo> questionsEnumerable)
        {
            List<QuestionInfo> questionsAndAnswers = courseService.GetAllCourseQuestions((string)Session["currentCourse"])
                                                   .Select(x => new QuestionInfo()
                                                   {
                                                       Qstn = x.Qstn,
                                                       Answers = (x.Answers).Split('/'),
                                                       RightAnswer = x.RightAnswer
                                                   }).ToList();
            var scoreToPass = courseService.GetCourseInfoDataByName((string)Session["currentCourse"]);
            int pointsPerQuestion = (scoreToPass.ScoreToPass / questionsAndAnswers.Count)+15;
            ExamResults examResults = new ExamResults() { ScoreToPass = scoreToPass.ScoreToPass};
            foreach (var qstn in questionsEnumerable)
            {
                if (questionsAndAnswers
                    .Where(x=>x.Qstn==qstn.Qstn)
                    .Select(x => x.RightAnswer)
                    .Contains(qstn.SelectedAnswer)) 
                {
                    examResults.Score += pointsPerQuestion;
                }
            }

            if (examResults.Score >= examResults.ScoreToPass)
            {
                examResults.Pass = true;
                
            }
            await courseService.SetCourseCompletionRate((string)Session["currentCourse"], examResults.Pass);
            return this.View("Results",examResults);
        }

        //public ActionResult Results(ExamResults examResults)
        //{
        //    return this.View(examResults)
        //}

        [ChildActionOnly]
        public ActionResult Questions(string name)
        {
            List<QuestionInfo> questionsAndAnswers = courseService.GetAllCourseQuestions(name)
                                                               .Select(x => new QuestionInfo()
                                                               {
                                                                   Qstn = x.Qstn,
                                                                   Answers = (x.Answers).Split('/'),
                                                                   RightAnswer = x.RightAnswer
                                                               }).ToList();
            Session["currentCourse"] = name;
            return this.PartialView("_Questions",questionsAndAnswers);
        }

        [ChildActionOnly]
        public ActionResult Slides(string name, string courseName)
        {
            IEnumerable<SlideImage> slideImages = courseService.GetAllCourseSlides(name)
                                                    .Select(x => new SlideImage()
                                                    {
                                                        Order = x.Order,
                                                        ImageBase64 = Convert.ToBase64String(x.ImageBinary)
                                                    });
            ViewBag.HiddenInput = courseName;
            return this.PartialView("_Slides", slideImages);
        }

    }
}