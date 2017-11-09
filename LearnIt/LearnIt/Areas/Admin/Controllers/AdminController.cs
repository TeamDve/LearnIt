using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnIt.Areas.Admin.Models;
using LearnIt.Data.Context;
using LearnIt.Data.Services.Contracts;
using System.IO;
using LearnIt.Data.Models;
using System.Threading.Tasks;

namespace LearnIt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly IJsonParserService jsonParser;
        private readonly ICourseService courseService;

        public AdminController(ApplicationUserManager userManager, IJsonParserService jsonParser, ICourseService courseService)
        {
            this.userManager = userManager;
            this.jsonParser = jsonParser;
            this.courseService = courseService;
        }

        public ActionResult ViewUsers()
        {
            var usersViewModel = this.userManager
                .Users
                .Select
                (u => new UserViewModel()
                {
                    Username = u.UserName
                }).ToList();
            return this.View(usersViewModel);
        }

        [HttpGet]
        public ActionResult UploadCourse()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadCourse(HttpPostedFileBase file)
        {
            if (file == null)
            {
               return RedirectToAction("ViewUsers");
            }

            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] binData = b.ReadBytes(file.ContentLength);

            var test = jsonParser.Execute(binData);
            await courseService.AddCourseToDb(test);

            return this.View(); 
        }

        public ActionResult AssignCourse()
        {
            return this.View();
        }
    }
}