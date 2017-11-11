using LearnIt.Data.Context;
using LearnIt.Data.DataModels;
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
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext cannot be null");
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin use only
        public IEnumerable<Course> GetAllCourses()
        {
            var list = dbContext.Courses.Select(x=>x).ToList();
            return list;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin && User use
        public IEnumerable<Course> GetUserCourses(string username)
        {
            var user = GetUserByName(username);

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
            var course = dbContext.Courses.FirstOrDefault(x => x.Id == courseId);

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
        public async Task AddCourseToDb(Course courseToAdd)
        {
            dbContext.Courses.Add(courseToAdd);
            await ExecuteQuery();
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        public async Task AssignCourseToUser(int courseId, string username, DateTime date, int status)
        {
            var course = dbContext.Courses.FirstOrDefault(x => x.Id == courseId);
            if (course == null)
            {
                throw new ArgumentException($"course does not exist");
            }
            var user = GetUserByName(username);
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
        public async Task UnassignCourseFromUser(int courseId, string username)
        {
            var course = dbContext.Courses.FirstOrDefault(x => x.Id == courseId);
            if(course == null)
            {
                throw new ArgumentException("no course found");
            }
            var user = GetUserByName(username);
            UserCourse usrFromCourse = dbContext.UsersCourses.First(x => x.UserId == user.Id && x.CourseId == courseId);
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

        public IEnumerable<UserCourseInfo> GetUsersCourseInfo(string username)
        {
            var user = GetUserByName(username);

            List<UserCourse> usersCourses = dbContext.UsersCourses.Where(x => x.UserId == user.Id).Select(x => x).ToList();
            List<UserCourseInfo> resultList = new List<UserCourseInfo>();
            if (usersCourses.Count == 0)
            {
                return resultList;
            }
            int courseId = usersCourses.First().CourseId;
            var course = dbContext.Courses.Where(x => x.Id == courseId).Select(x => x.Name).ToList<string>();

            foreach(var item in usersCourses)
            {
                resultList.Add(new UserCourseInfo()
                {
                    Name = course[0],
                    Id = item.Id,
                    Status = item.Status,
                    AssignmentDate = item.AssignmentDate,
                    DueDate = item.DueDate,
                    CompletionDate = item.CompletionDate
                });
            }

            return resultList;
        }

        private ApplicationUser GetUserByName(string username)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                throw new ArgumentException($"{username} does not exist");
            }
            return user;
        }

        private async Task ExecuteQuery()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
