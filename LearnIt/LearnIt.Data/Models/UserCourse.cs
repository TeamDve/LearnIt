using System;
using System.ComponentModel.DataAnnotations.Schema;
using LearnIt.Data.Enums;

namespace LearnIt.Data.Models
{
    public class UserCourse
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime AssignmentDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public bool IsMandatory { get; set; }

        public CourseStatus Status { get; set; }

        public bool areQuestionsOpened { get; set; }
    }
}
