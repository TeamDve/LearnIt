using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnIt.Areas.Projects.Controllers
{
    public class ProjectsController : Controller
    {

        public ActionResult AllProjects()
        {
            return this.View();
        }

        public ActionResult MyProjects()
        {
            return this.View();
        }
    }
}