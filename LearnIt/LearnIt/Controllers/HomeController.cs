using System.Web.Mvc;
using LearnIt.Data.Services.Contracts;
using System.Threading.Tasks;

namespace LearnIt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService courseService;


        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            //await courseService.AssingCourseToUser(2, "59b37e0f-ff22-47ba-90da-405755ea2dbf", System.DateTime.Now,0);
            return View();
        }
    }
}