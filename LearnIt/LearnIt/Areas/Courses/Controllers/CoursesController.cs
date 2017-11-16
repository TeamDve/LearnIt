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
        private const string pending = "Pending";
        private const string started = "Started";
        private const string completed = "Completed";
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
        public async Task<ActionResult> StartPresentation(string name)
        {
            ViewBag.areQuestionsAvailable = courseService.GetCourseCompletionRate(name);
            ViewBag.HiddenInput = name;
            Session["currentCourse"] = name;
            await courseService.ChangeUserCourseStatus(name, started);
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
            var courseName = (string)Session["currentCourse"];
            List<QuestionInfo> questionsAndAnswers = courseService.GetAllCourseQuestions(courseName)
                                                   .Select(x => new QuestionInfo()
                                                   {
                                                       Qstn = x.Qstn,
                                                       Answers = (x.Answers).Split('/'),
                                                       RightAnswer = x.RightAnswer
                                                   }).ToList();
            var scoreToPass = courseService.GetCourseInfoDataByName(courseName);
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
                //await courseService.ChangeUserCourseStatus(courseName, completed);
            }
            await courseService.SetCourseCompletionRate(courseName, examResults.Pass);
            return this.PartialView("_Results",examResults);
        }


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