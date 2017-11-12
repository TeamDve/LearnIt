using LearnIt.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIt.Data.DataModels
{
    public class UserCourseInfo
    {
        public string Name { get; set; }

        //UserCourseId
        public int Id { get; set; }

        public CourseStatus Status { get; set; }

        public bool isMandatory { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? CompletionDate { get; set; }
    }
}
