using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIt.Data.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5)]
        public string Qstn { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 5)]
        public string Answers { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5)]
        public string RightAnswer { get; set; }

        public int CourseId { get; set; }
    }
}
