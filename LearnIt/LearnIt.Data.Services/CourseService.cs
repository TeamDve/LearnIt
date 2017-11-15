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
        //Tested
        public CourseService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException("dbContext cannot be null");
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin use only
        private IQueryable<Course> GetAllCoursesQuery()
        {
            var list = this.dbContext.Courses.Select(x => x);
            return list;
        }
        //Tested
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
                            .ToList();
        }
        public IEnumerable<CourseSlidesBinary> GetAllCourseSlides(string courseName)
        {

            var enumerableOfImages = this.GetAllCoursesQuery()
                            .Where(x => x.Name == courseName)
                            .Select(x => x.Images)
                            .First();

            List<CourseSlidesBinary> listOfImages = new List<CourseSlidesBinary>();
            foreach (var item in enumerableOfImages)
            {
                listOfImages.Add(new CourseSlidesBinary()
                {
                    ImageBinary = item.ImageBinary
                });
            }
            listOfImages.Sort((a, b) => (a.Order.CompareTo(b.Order)));
            return listOfImages;
        }

        public bool GetCourseCompletionRate(string courseName)
        {
            bool result = this.dbContext.UsersCourses
                                        .Where(x => x.Course.Name == courseName)
                                        .Select(x => x.areQuestionsOpened)
                                        .First();
            return result;
        }

        public async Task SetCourseCompletionRate(string courseName, bool completed)
        {
            UserCourse course = this.dbContext.UsersCourses
                                        .Where(x => x.Course.Name == courseName)
                                        .First();
            if (completed)
            {
                course.Status = CourseStatus.Completed;
            }
            else
            {
                course.Status = CourseStatus.Pending;
                course.areQuestionsOpened = false;
            }
            
            await ExecuteQuery();
        }

        public async Task TryCompleteCourse(string courseName)
        {
            this.dbContext.UsersCourses
                                        .Where(x => x.Course.Name == courseName)
                                        .First()
                                        .areQuestionsOpened = true;
            await ExecuteQuery();
        }

        public IEnumerable<CourseQuestions> GetAllCourseQuestions(string courseName)
        {

            var enumerableOfQuestions = this.GetAllCoursesQuery()
                            .Where(x => x.Name == courseName)
                            .Select(x => x.Questions)
                            .First();

            List<CourseQuestions> listOfQuestions = new List<CourseQuestions>();
            foreach (var item in enumerableOfQuestions)
            {
                listOfQuestions.Add(new CourseQuestions()
                {
                    Qstn = item.Qstn,
                    Answers = item.Answers,
                    RightAnswer = item.RightAnswer
                });
            }
            return listOfQuestions;
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin && User use
        //Tested
        public IEnumerable<Course> GetUserCourses(string username)
        {
            var user = GetUserByName(username);

            var listOfUsersWithCourses = this.dbContext
                .UsersCourses
                .Where(x => x.UserId == user.Id)
                .Select(x => x.CourseId)
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
        //Tested
        public Course GetCourseById(int courseId)
        {
            var course = this.dbContext
                .Courses
                .FirstOrDefault(x => x.Id == courseId);

            return course;
        }

        public CourseInfoData GetCourseInfoDataByName(string courseName)
        {
            return this.dbContext.Courses
                            .Where(x => x.Name == courseName)
                            .Select(x => new CourseInfoData()
                            {
                                DateAdded = x.DateAdded,
                                Description = x.Description,
                                Name = x.Name,
                                ScoreToPass = x.ScoreToPass
                            })
                            .First();
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Home
        //Tested
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
        //Tested
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
        //Tested
        public async Task AddCourseToDb(Course courseToAdd)
        {
            dbContext.Courses.Add(courseToAdd);
            await ExecuteQuery();
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        //Tested
        public async Task AssignCourseToUser(
            string courseName,
            string username,
            DateTime dueDate,
            bool isMandatory)
        {


            var course = dbContext.Courses.FirstOrDefault(c => c.Name == courseName) ??
                         throw new ArgumentNullException(null,"The course is not found!");
           
           

            var user = this.GetUserByName(username);
            if (!user.UsersCourses
                .Any(courses =>courses.Course.Name==courseName 
                && courses.DueDate==dueDate))
            {
                UserCourse usrToCourse = new UserCourse()
                {
                    CourseId = course.Id,
                    UserId = user.Id,
                    DueDate = dueDate,
                    Status = CourseStatus.Pending,
                    AssignmentDate = DateTime.Now,
                    IsMandatory = isMandatory
                };
                this.dbContext.UsersCourses.Add(usrToCourse);
            }
            else
            {
                    throw new ArgumentNullException(null,"A course with this date has been addedto this user already.");
            }
            await ExecuteQuery();
        }

        //Change Course to DataModel if data must be hidden OR assign to same models with fewer details in them
        //Admin
        public async Task DeassignCourseFromUser(string courseName, string username,DateTime dueDate)
        {

           var checkUser = this.GetUserByName(username);

           var user = this.GetUserByName(username);

           if(user.UsersCourses.Any(courses=>courses.Course.Name == courseName && courses.DueDate==dueDate))
           {
                var course = user.UsersCourses
                    .Where(courses => courses.Course.Name == courseName 
                    && courses.DueDate == dueDate).FirstOrDefault();

                this.dbContext.UsersCourses.Remove(course);
            }
            else
            {
                throw new ArgumentNullException(null, "No such course was found");
            }
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
            var affectedUsers = this.dbContext.Users
                            .Where(u => u.Position.Name == posName && u.Department.Name == depName)
                            .Select(u => u.Id)
                            .ToList<string>();
            if (affectedUsers.Count <= 0)
            {
                throw new ArgumentNullException(null, "There are no users that have that position and that department.");
            }

            try
            {
                var checkCourse = dbContext.Courses.First(c => c.Name == courseName);
            }
            catch(Exception)
            {
                throw new ArgumentNullException(null, "No such course exists.");
            }

            var course = dbContext.Courses.First(c => c.Name == courseName);

            foreach (var usr in affectedUsers)
            {
                var user = this.dbContext.Users.Where(u => u.Id == usr).Single();
                if (!user.UsersCourses
                .Any(courses => courses.Course.Name == courseName
                && courses.DueDate == dueDate))
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
            }
            await ExecuteQuery();

        }

        public async Task DeassignExistingCourseToPosAndDept(
            string courseName,
            string depName,
            string posName,
            DateTime dueDate)
        {
            var affectedUsers = dbContext.Users
                            .Where(u => u.Position.Name == posName && u.Department.Name == depName)
                            .Select(u => u.UserName)
                            .ToList<string>();

            if (affectedUsers.Count <= 0)
            {
                throw new ArgumentNullException(null, "There are no users that have that position and that department.");
            }

            foreach (var username in affectedUsers)
            {
                var user = this.GetUserByName(username);

                if (user.UsersCourses
                    .Any(courses => courses.Course.Name == courseName
                    && courses.DueDate == dueDate))
                {
                    var course = user.UsersCourses
                        .Where(courses => courses.Course.Name == courseName
                        && courses.DueDate == dueDate).First();

                    this.dbContext.UsersCourses.Remove(course);
                }
                await ExecuteQuery();

            }


            await ExecuteQuery();

        }
       //Tested
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
        //Tested
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
        //Tested
        public IEnumerable<NameHolder> ReturnAllCourseNames()
        {
            IEnumerable<NameHolder> courseNames = this.dbContext
                .Courses
                .Select
                (c => new NameHolder()
                {
                    Names = c.Name
                })
                .ToList();
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
