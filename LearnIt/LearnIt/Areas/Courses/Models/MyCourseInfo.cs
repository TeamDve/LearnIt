using System;

namespace LearnIt.Areas.Courses.Models
{
    public class MyCourseInfo
    {
        public string Name { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsMandatory { get; set; }
    }
}