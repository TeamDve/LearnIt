using LearnIt.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnIt.Areas.Admin.Models
{
    public class CourseToUser
    {

        public string CourseName { get; set; }

        public string Username { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsMandatory { get; set; }

    }
}