using System.Linq;
using System.Web.Mvc;
using LearnIt.Data.Services.Contracts;
using System.Threading.Tasks;
using LearnIt.Models;

namespace LearnIt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService courseService;


        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [OutputCache(CacheProfile = "Home")]
        public ActionResult Index()
        {

            var viewmodel = this.courseService.GetLast(3).Select(x => new CourseViewModels()
                {
                    Name = x.Name,
                    Description = x.Description,
                    DateAdded = x.DateAdded
                }
            );

            return View(viewmodel);
        }

        public ActionResult LastCourses()
        {
            var viewmodel = this.courseService.GetLast(3).Select(x => new CourseViewModels()
                {
                    Name = x.Name,
                    Description =x.Description,
                    DateAdded = x.DateAdded
                }
            );

            return View(viewmodel);
        }
            

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //courseService.GetUsersCourseInfo("userthree@userthree.userthree");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";


            return View();
        }
    }
}