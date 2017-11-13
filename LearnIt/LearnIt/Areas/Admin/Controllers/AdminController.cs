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
using LearnIt.Data.Enums;

namespace LearnIt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IJsonParserService jsonParser;
        private readonly ICourseService courseService;
        private readonly IUserServices userServices;
        private readonly IDepartmenService departmentService;
        private readonly IPossitionService possitionService;

        public AdminController(
            IJsonParserService jsonParser,
            ICourseService courseService,
            IUserServices userServices,
            IDepartmenService departmentService,
            IPossitionService possitionService)
        {
            this.jsonParser = jsonParser;
            this.courseService = courseService;
            this.userServices = userServices;
            this.departmentService = departmentService;
            this.possitionService = possitionService;
        }

        public ActionResult ViewUsers()
        {
            var usersViewModel = this.userServices.ReturnAllUserNames();
            ViewBag.UserNames = usersViewModel;
            return this.View();
        }

        public ActionResult LoadUser(string username)
        {
            var user = this.userServices.ReturnUserByUsername(username);
            var userViewModel = UserViewModel.Create.Compile()(user);
            userViewModel.IsAdmin = this.userServices.IsUserAdmin(user.Id);

            ViewBag.PendingCourses = this.courseService
                .GetUsersCourseInfoByStatus(
                user.UserName,
                CourseStatus.Pending);

            ViewBag.StartedCourses = this.courseService
                 .GetUsersCourseInfoByStatus(
                 user.UserName,
                 CourseStatus.Started);

            ViewBag.CompletedCourses = this.courseService
                .GetUsersCourseInfoByStatus(
                user.UserName,
                CourseStatus.Completed);


            return this.View("_LoadUser", userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserViewModel userViewModel)
        {
            if (userViewModel.IsAdmin)
            {
                await this.userServices.AsignUserToAdmin(userViewModel.Id);
            }
            else
            {
                await this.userServices.DeasignUserFromAdmin(userViewModel.Id);
            }

            return this.RedirectToAction("ViewUsers");
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


        public ViewResult SinglePersonCourseAssign()
        {
            UserNameAndProjectNameModel userAndProjectNames = new UserNameAndProjectNameModel
            {
                UsernameList = this.userServices.ReturnAllUserNames(),
                CourseNameList = this.courseService.ReturnAllCourseNames()
            };

            ViewBag.userAndProjectNames = userAndProjectNames;

            return this.View("_SinglePersonCourseAssign");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SinglePersonCourseAsign(CourseToUser singleCourseAsignModel)
        {
            await this.courseService.AssignCourseToUser(
                singleCourseAsignModel.CourseName,
                singleCourseAsignModel.Username,
                singleCourseAsignModel.DueDate,
                singleCourseAsignModel.IsMandatory);

            return this.RedirectToAction("AssignCourse");
        }

        public ActionResult BulkCourseAssign(string something)
        {
            DepartPossitionAndCourseNames courseDepPosNames = new DepartPossitionAndCourseNames
            {
                DepartmentList = this.departmentService.ReturnAllDepartmentNames(),
                PossitionList=this.possitionService.ReturnAllPossitionNames(),
                CourseNameList=this.courseService.ReturnAllCourseNames()
            };

            ViewBag.courseDepPosNames = courseDepPosNames;

            return this.View("_BulkCourseAssign");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BulkCourseAssign(CourseToPosDep singleCourseAsignModel)
        {
            await this.courseService.AssignExistingCourseToPosAndDept(
                singleCourseAsignModel.CourseName,
                singleCourseAsignModel.Department,
                singleCourseAsignModel.Possition,
                singleCourseAsignModel.DueDate,
                singleCourseAsignModel.IsMandatory);

            return this.RedirectToAction("AssignCourse");
        }

        public ActionResult DeassignCourse()
        {
            return this.View();
        }

        public ViewResult SinglePersonCourseDeassign()
        {
            UserNameAndProjectNameModel userAndProjectNames = new UserNameAndProjectNameModel
            {
                UsernameList = this.userServices.ReturnAllUserNames(),
                CourseNameList = this.courseService.ReturnAllCourseNames()
            };

            ViewBag.userAndProjectNames = userAndProjectNames;

            return this.View("_SinglePersonCourseAsign");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SinglePersonCourseDeassign(CourseToUser singleCourseAsignModel)
        {
            await this.courseService.AssignCourseToUser(
                singleCourseAsignModel.CourseName,
                singleCourseAsignModel.Username,
                singleCourseAsignModel.DueDate,
                singleCourseAsignModel.IsMandatory);

            return this.RedirectToAction("AssignCourse");
        }
    }
}