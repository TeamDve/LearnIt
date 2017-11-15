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
using Bytes2you.Validation;

namespace LearnIt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IJsonParserService jsonParser;
        private readonly ICourseService courseService;
        private readonly IUserServices userServices;
        private readonly IDepartmenService departmentService;
        private readonly IPositionService possitionService;

        public AdminController(
            IJsonParserService jsonParser,
            ICourseService courseService,
            IUserServices userServices,
            IDepartmenService departmentService,
            IPositionService possitionService)
        {

            Guard.WhenArgument(jsonParser, "jsonParser").IsNull().Throw();
            Guard.WhenArgument(courseService, "courseService").IsNull().Throw();
            Guard.WhenArgument(userServices, "userServices").IsNull().Throw();
            Guard.WhenArgument(departmentService, "departmentService").IsNull().Throw();
            Guard.WhenArgument(possitionService, "possitionService").IsNull().Throw();

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

            try
            {

                BinaryReader b = new BinaryReader(file.InputStream);
                byte[] binData = b.ReadBytes(file.ContentLength);

                var test = this.jsonParser.Execute(binData);
                await this.courseService.AddCourseToDb(test);
            }
            catch (Exception)
            {
                ViewBag.Error = "File was in the wrong format";
                return this.View();
            }

            ViewBag.Success = "You have succesfully uploaded a course!";
            return this.View();
        }

        public ActionResult AssignCourse()
        {
            return this.View();
        }


        public ActionResult SinglePersonCourseAssign()
        {
            UserNameAndProjectNameModel userAndProjectNames = new UserNameAndProjectNameModel
            {
                UsernameList = this.userServices.ReturnAllUserNames(),
                CourseNameList = this.courseService.ReturnAllCourseNames()
            };

            ViewBag.userAndProjectNames = userAndProjectNames;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SinglePersonCourseAssign(CourseToUser singleCourseAsignModel)
        {
            try
            {
                await this.courseService.AssignCourseToUser(
                singleCourseAsignModel.CourseName,
                singleCourseAsignModel.Username,
                singleCourseAsignModel.DueDate,
                singleCourseAsignModel.IsMandatory);
            }
            catch (Exception ex)
            {
                UserNameAndProjectNameModel userAndProjectNames = new UserNameAndProjectNameModel
                {
                    UsernameList = this.userServices.ReturnAllUserNames(),
                    CourseNameList = this.courseService.ReturnAllCourseNames()
                };

                ViewBag.userAndProjectNames = userAndProjectNames;
                ViewBag.Error = ex.Message;

                return this.View(singleCourseAsignModel);
            }
            return this.RedirectToAction("AssignCourse");
        }

        public ActionResult BulkCourseAssign()
        {
            DepartPossitionAndCourseNames courseDepPosNames = new DepartPossitionAndCourseNames
            {
                DepartmentList = this.departmentService.ReturnAllDepartmentNames(),
                PossitionList = this.possitionService.ReturnAllPossitionNames(),
                CourseNameList = this.courseService.ReturnAllCourseNames()
            };

            ViewBag.courseDepPosNames = courseDepPosNames;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BulkCourseAssign(CourseToPosDep bulkCourseAsignModel)
        {
            try
            {
                await this.courseService.AssignExistingCourseToPosAndDept(
                    bulkCourseAsignModel.CourseName,
                    bulkCourseAsignModel.Department,
                    bulkCourseAsignModel.Possition,
                    bulkCourseAsignModel.DueDate,
                    bulkCourseAsignModel.IsMandatory);
            }
            catch (Exception ex)
            {
                DepartPossitionAndCourseNames courseDepPosNames = new DepartPossitionAndCourseNames
                {
                    DepartmentList = this.departmentService.ReturnAllDepartmentNames(),
                    PossitionList = this.possitionService.ReturnAllPossitionNames(),
                    CourseNameList = this.courseService.ReturnAllCourseNames()
                };

                ViewBag.courseDepPosNames = courseDepPosNames;
                ViewBag.Error = ex.Message;

                return this.View(bulkCourseAsignModel);
            }

            return this.RedirectToAction("AssignCourse");
        }

        public ActionResult DeassignCourse()
        {
            return this.View();
        }

        public ActionResult SinglePersonCourseDeassign()
        {
            UserNameAndProjectNameModel userAndProjectNames = new UserNameAndProjectNameModel
            {
                UsernameList = this.userServices.ReturnAllUserNames(),
                CourseNameList = this.courseService.ReturnAllCourseNames()
            };

            ViewBag.userAndProjectNames = userAndProjectNames;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SinglePersonCourseDeassign(CourseToUserDeassign singleCourseDeassignModel)
        {
            try
            {
                await this.courseService.DeassignCourseFromUser(
                    singleCourseDeassignModel.CourseName,
                    singleCourseDeassignModel.Username,
                    singleCourseDeassignModel.DueDate);
            }
            catch (Exception ex)
            {
                UserNameAndProjectNameModel userAndProjectNames = new UserNameAndProjectNameModel
                {
                    UsernameList = this.userServices.ReturnAllUserNames(),
                    CourseNameList = this.courseService.ReturnAllCourseNames()
                };

                ViewBag.userAndProjectNames = userAndProjectNames;
                ViewBag.Error = ex.Message;

                return this.View(singleCourseDeassignModel);
            }

            return this.RedirectToAction("DeassignCourse");
        }

        public ActionResult BulkCourseDeassign()
        {
            DepartPossitionAndCourseNames courseDepPosNames = new DepartPossitionAndCourseNames
            {
                DepartmentList = this.departmentService.ReturnAllDepartmentNames(),
                PossitionList = this.possitionService.ReturnAllPossitionNames(),
                CourseNameList = this.courseService.ReturnAllCourseNames()
            };

            ViewBag.courseDepPosNames = courseDepPosNames;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BulkCourseDeassign(CourseToPosDepDeassign bulkCourseDeassignModel)
        {
            try
            {
                await this.courseService.DeassignExistingCourseToPosAndDept(
                    bulkCourseDeassignModel.CourseName,
                    bulkCourseDeassignModel.Department,
                    bulkCourseDeassignModel.Possition,
                    bulkCourseDeassignModel.DueDate);
            }
            catch (Exception ex)
            {
                DepartPossitionAndCourseNames courseDepPosNames = new DepartPossitionAndCourseNames
                {
                    DepartmentList = this.departmentService.ReturnAllDepartmentNames(),
                    PossitionList = this.possitionService.ReturnAllPossitionNames(),
                    CourseNameList = this.courseService.ReturnAllCourseNames()
                };

                ViewBag.courseDepPosNames = courseDepPosNames;
                ViewBag.Error = ex.Message;

                return this.View(bulkCourseDeassignModel);
            }

            return this.RedirectToAction("DeassignCourse");
        }
    }
}