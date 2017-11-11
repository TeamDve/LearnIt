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
                (u => new UserViewModelNameOnly()
                {
                    Username = u.UserName
                }).ToList();
            return this.View(usersViewModel);
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

        public async Task<ActionResult> LoadUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var userViewModel = UserViewModel.Create.Compile()(user);
            userViewModel.IsAdmin = await this.userManager.IsInRoleAsync(user.Id, "Admin");

            return this.View("_LoadUser", userViewModel);
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

        public  ViewResult SinglePersonCourse()
        {
        //    UserNameAndProjectNameModel usernamesAndProjectNames = new UserNameAndProjectNameModel();
        //    usernamesAndProjectNames.UsernameList = this.userManager
        //        .Users
        //        .Select(u => u.UserName)
        //        .ToList();
        //    i need work with the db here

        //    usernamesAndProjectNames.ProjectNameList= 

            return this.View("_SinglePersonCourse");
        }

        public async Task<ActionResult> BulkCourse(string something)
        {
            return this.View("_BulkCourse");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddCourseToUser()


        //    return this.RedirectToAction();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddCOurseToBulk()


        //    return this.RedirectToAction();
        //}
    }
}