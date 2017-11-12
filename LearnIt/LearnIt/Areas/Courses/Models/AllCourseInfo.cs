using System;

namespace LearnIt.Areas.Courses.Models
{
    public class AllCourseInfo
    {
        public DateTime DateAdded { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ScoreToPass { get; set; }
    }
}
