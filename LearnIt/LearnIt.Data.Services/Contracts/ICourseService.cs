using LearnIt.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LearnIt.Data.DataModels;

namespace LearnIt.Data.Services.Contracts
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();

        IEnumerable<Course> GetUserCourses(string username);

        Course GetCourseById(int courseId);

        Task AddCourseToDb(string name, string desc, DateTime date, int scoreToPass, bool required);

        Task AddCourseToDb(Course courseToAdd);

        Task AssignCourseToUser(int courseId, string username, DateTime date, int status);

        Task UnassignCourseFromUser(int courseId, string username);

        Task AssignExistingCourseToPosAndDept(int courseId, int deptId, int posId, DateTime date, int status);

        IEnumerable<UserCourseInfo> GetUsersCourseInfo(string username);
    }
}
