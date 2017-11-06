using System;
using System.ComponentModel.DataAnnotations.Schema;

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

        public DateTime DueDate { get; set; }

        public int Status { get; set; }
    }
}
