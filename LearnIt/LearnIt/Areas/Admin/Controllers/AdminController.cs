using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnIt.Areas.Admin.Models;

namespace LearnIt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;

        public AdminController(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
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
        public string UploadCourse(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "Error";
            }
            return file.ContentType;
        }

        public ActionResult AssignCourse()
        {
            return this.View();
        }
    }
}