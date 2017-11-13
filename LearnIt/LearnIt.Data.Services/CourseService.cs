using LearnIt.Data.Context;
using LearnIt.Data.DataModels;
using LearnIt.Data.Enums;
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
        private IQueryable<Course> GetAllCoursesQuery()
        {
            var list = this.dbContext.Courses.Select(x=>x);
            return list;
        }
        public IEnumerable<CourseInfoData> GetAllCourses()
        {
            
        return this.GetAllCoursesQuery()
                        .Select(x => new CourseInfoData()
                            {
                                Name = x.Name,
                                DateAdded = x.DateAdded,
                                Description = x.Description,
                                ScoreToPass = x.ScoreToPass

                            })
                        .ToList(); ;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin && User use
        public IEnumerable<Course> GetUserCourses(string username)
        {
            var user = GetUserByName(username);

            var listOfUsersWithCourses = this.dbContext
                .UsersCourses
                .Where(x => x.UserId == user.Id)
                .Select(x=>x.CourseId)
                .ToList();

            if (listOfUsersWithCourses == null)
            {
                listOfUsersWithCourses = new List<int>();
            }

            var listOfUsersCourses = this.dbContext
                .Courses
                .Select(x => x)
                .Where(x => listOfUsersWithCourses.Contains(x.Id))
                .ToList();

            return listOfUsersCourses;
        }

      
        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin && User use
        public Course GetCourseById(int courseId)
        {
            var course = this.dbContext
                .Courses
                .FirstOrDefault(x => x.Id == courseId);

            return course;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Home
        public IEnumerable<Course> GetLast(int count)
        {
            var list = this.dbContext
                .Courses
                .Select(x => x)
                .OrderByDescending(x => x.DateAdded)
                .Take(5)
                .ToList();

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
        public async Task AssignCourseToUser(
            string courseName,
            string username,
            DateTime dueDate,
            bool isMandatory)
        {
            var course = dbContext.Courses.First(c => c.Name == courseName);
            var user = this.GetUserByName(username);
            UserCourse usrToCourse = new UserCourse()
            {
                CourseId = course.Id,
                UserId = user.Id,
                DueDate = dueDate,
                Status = CourseStatus.Pending,
                AssignmentDate = DateTime.Now,
                IsMandatory = isMandatory
            };
            if (dbContext.UsersCourses.Single(
                c=>c.CourseId==usrToCourse.CourseId
                && c.UserId==usrToCourse.UserId
                && c.DueDate==usrToCourse.DueDate) == null)
            {
                dbContext.UsersCourses.Add(usrToCourse);
            }
            await ExecuteQuery();
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        public async Task UnassignCourseFromUser(int courseId, string username)
        {
            var course = dbContext
                .Courses
                .First(x => x.Id == courseId);

            var user = GetUserByName(username);

            UserCourse usrFromCourse = dbContext
                .UsersCourses
                .First(x => x.UserId == user.Id && x.CourseId == courseId);

            this.dbContext.UsersCourses.Remove(usrFromCourse);
            await ExecuteQuery();
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        public async Task AssignExistingCourseToPosAndDept(
            string courseName,
            string depName,
            string posName,
            DateTime dueDate,
            bool isMandatory)
        {
            var affectedUsers = dbContext.Users
                            .Where(u => u.Position.Name == posName && u.Department.Name == depName)
                            .Select(u => u.Id)
                            .ToList<string>();

            var course = dbContext.Courses.First(c => c.Name == courseName);
            foreach (var usr in affectedUsers)
            {
                UserCourse usrToCourse = new UserCourse()
                {
                    CourseId = course.Id,
                    UserId = usr,
                    DueDate = dueDate,
                    Status = CourseStatus.Pending,
                    AssignmentDate = DateTime.Now,
                    IsMandatory = isMandatory
                };

                this.dbContext.UsersCourses.Add(usrToCourse);
            }
            await ExecuteQuery();

        }

        public async Task DeassignExistingCourseToPosAndDept(
            string courseName,
            string depName,
            string posName,
            DateTime dueDate,
            bool isMandatory)
        {
            var affectedUsers = dbContext.Users
                            .Where(u => u.Position.Name == posName && u.Department.Name == depName)
                            .Select(u => u.Id)
                            .ToList<string>();

            var course = dbContext.Courses.First(c => c.Name == courseName);
            foreach (var usr in affectedUsers)
            {
                UserCourse usrToCourse = new UserCourse()
                {
                    CourseId = course.Id,
                    UserId = usr,
                    DueDate = dueDate,
                    Status = CourseStatus.Pending,
                    AssignmentDate = DateTime.Now,
                    IsMandatory = isMandatory
                };
                if (dbContext.UsersCourses.Single(
                c => c.CourseId == usrToCourse.CourseId
                && c.UserId == usrToCourse.UserId
                && c.DueDate == usrToCourse.DueDate) != null)
                {
                    this.dbContext.UsersCourses.Remove(usrToCourse);
                }
            }
            await ExecuteQuery();

        }

        public IEnumerable<UserCourseInfo> GetUsersCourseInfo(string username)
        {
            var user = GetUserByName(username);

            List<UserCourseInfo> resultList = dbContext.UsersCourses
                .Where(u => u.UserId == user.Id)
                .Select(x => new UserCourseInfo()
                                {
                                    Name = x.Course.Name,
                                    Id = x.Id,
                                    Status = x.Status,
                                    AssignmentDate = x.AssignmentDate,
                                    DueDate = x.DueDate,
                                    isMandatory = x.IsMandatory,
                                    CompletionDate = x.CompletionDate
                                })
                .ToList();
            return resultList;
        }

        public IEnumerable<UserCourseInfo> GetUsersCourseInfoByStatus(string username, CourseStatus status)
        {
            var completedCourses = this.GetUsersCourseInfo(username)
                .Where(c => c.Status == status)
                .ToList();

            if (completedCourses.Count() <= 0)
            {
                return null;
            }
            else
            {
                return completedCourses;
            }
        }

        public IEnumerable<NameHolder> ReturnAllCourseNames()
        {
            IEnumerable<NameHolder> courseNames = this.dbContext
                .Courses
                .Select
                (c => new NameHolder()
                {
                    Names = c.Name
                }).ToList();
            return courseNames;
        }

        private ApplicationUser GetUserByName(string username)
        {
            var user = this.dbContext
                .Users.
                FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                throw new ArgumentException($"{username} does not exist");
            }
            return user;
        }

        private async Task ExecuteQuery()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
