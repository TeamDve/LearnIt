﻿using System;
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

        public ActionResult EditUsers()
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
        public ActionResult UploadProject()
        {
            return this.View();
        }

        [HttpPost]
        public string UploadProject(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "Error";
            }
            return file.ContentType;
        }

        public ActionResult AssignProject()
        {
            return this.View();
        }
    }
}