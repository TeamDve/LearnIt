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
                (UserViewModel.Create).ToList();
            return this.View(usersViewModel);
        }

        [HttpGet]
        public ActionResult UploadCourse()
        {
            return this.View();
        }

        public async Task<ActionResult> LoadUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var userViewModel = UserViewModel.Create.Compile()(user);
            userViewModel.IsAdmin = await this.userManager.IsInRoleAsync(user.Id, "Admin");

            return this.View("_LoadUser", userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserViewModel userViewModel)
        {
            if (userViewModel.IsAdmin)
            {
                await this.userManager.AddToRoleAsync(userViewModel.Id, "Admin");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(userViewModel.Id, "Admin");
            }

            return this.RedirectToAction("ViewUsers");
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