using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnIt.Areas.Courses.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {

        public ActionResult AllCourses()
        {
            return this.View();
        }

        public ActionResult MyCourses()
        {
            return this.View();
        }
    }
}