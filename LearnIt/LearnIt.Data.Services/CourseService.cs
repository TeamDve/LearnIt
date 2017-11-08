using LearnIt.Data.Context;
using LearnIt.Data.Models;
using LearnIt.Data.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnIt.Data.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext dbContext;

        public CourseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin use only
        public IEnumerable<Course> GetAllCourses()
        {
            var list = dbContext.Courses.Select(x=>x).ToList();
            if (list == null)
            {
                list = new List<Course>();
            }
            return list;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin && User use
        public IEnumerable<Course> GetUserCourses(string userId)
        {
            var user = dbContext.Users.First(x => x.Id == userId);

            var listOfUsersWithCourses = dbContext.UsersCourses.Where(x => x.UserId == user.Id).Select(x=>x.CourseId).ToList();

            if (listOfUsersWithCourses == null)
            {
                listOfUsersWithCourses = new List<int>();
            }

            var listOfUsersCourses = dbContext.Courses.Select(x => x).Where(x => listOfUsersWithCourses.Contains(x.Id)).ToList();

            return listOfUsersCourses;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin && User use
        public Course GetCourseById(int courseId)
        {
            var course = dbContext.Courses.First(x => x.Id == courseId);

            return course;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Home
        public IEnumerable<Course> GetLast(int count)
        {
            var list = dbContext.Courses.Select(x => x).OrderByDescending(x => x.DateAdded).Take(5).ToList();

            return list;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        public async Task AddCourseToDb(string name, string desc, DateTime date, int scoreToPass, bool required)
        {
            Course course = new Course()
            {
                Name = name,
                DateAdded = date,
                Required = required,
                Description = desc,
                ScoreToPass = scoreToPass
            };
            dbContext.Courses.Add(course);
            await ExecuteQuery();

        }
        
        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        public async Task AssignCourseToUser(int courseId, string userId, DateTime date, int status)
        {
            var course = dbContext.Courses.First(x => x.Id == courseId);
            var user = dbContext.Users.First(x => x.Id == userId);
            UserCourse usrToCourse = new UserCourse()
            {
                AssignmentDate = DateTime.Now,
                DueDate = date,
                ApplicationUser = user,
                Course = course,
                Status = 1
            };
            dbContext.UsersCourses.Add(usrToCourse);
            await ExecuteQuery();
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        public async Task UnassignCourseFromUser(int courseId, string userId)
        {
            var course = dbContext.Courses.First(x => x.Id == courseId);
            var user = dbContext.Users.First(x => x.Id == userId);
            UserCourse usrFromCourse = dbContext.UsersCourses.First(x => x.UserId == userId && x.CourseId == courseId);
            dbContext.UsersCourses.Remove(usrFromCourse);
            await ExecuteQuery();
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        public async Task AssignExistingCourseToPosAndDept(int courseId, int deptId, int posId, DateTime date, int status)
        {
            var affectedUsers = dbContext.Users
                            .Where(x => x.PositionId == posId && x.DepartmentId == deptId)
                            .Select(x => x.Id)
                            .ToList<string>();
            foreach(var usr in affectedUsers)
            {
                UserCourse usrCourseEntry = new UserCourse()
                {
                    CourseId = courseId,
                    UserId = usr,
                    DueDate = date,
                    Status = status
                };
                dbContext.UsersCourses.Add(usrCourseEntry);
            }
            await ExecuteQuery();
           
        }

        private async Task ExecuteQuery()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
