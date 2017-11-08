using LearnIt.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace LearnIt.Data.Services.Contracts
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();

        IEnumerable<Course> GetUserCourses(string userId);

        Course GetCourseById(int courseId);

        Task AddCourseToDb(string name, string desc, DateTime date, int scoreToPass, bool required);

        Task AssingCourseToUser(int courseId, string userId, DateTime date, int status);

        Task AssignExistingCourseToPosAndDept(int courseId, int deptId, int posId, DateTime date, int status);
    }
}
