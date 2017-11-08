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
            courseService.GetUsersCourseInfo("userthree@userthree.userthree");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";


            return View();
        }
    }
}