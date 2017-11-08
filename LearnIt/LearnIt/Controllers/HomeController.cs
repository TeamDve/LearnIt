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

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";
            await courseService.UnassignCourseFromUser(1, "a3245ccb-b660-44b3-877e-869b9e159bc6");
            return View();
        }
    }
}