﻿using LearnIt.Data.Models;
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

        Task AssignCourseToUser(string courseName, string username, DateTime date, bool isMandatory);

        Task UnassignCourseFromUser(int courseId, string username);

        Task AssignExistingCourseToPosAndDept(string courseName, string depName, string posName, DateTime dueDate, bool isMandatory);

        IEnumerable<UserCourseInfo> GetUsersCourseInfo(string username);

        IEnumerable<CourseCourseNames> ReturnAllCourseNames();
    }
}
