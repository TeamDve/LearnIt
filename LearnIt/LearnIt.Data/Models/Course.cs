using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIt.Data.Models
{
    public class Course
    {
        private ICollection<Question> questions;
        private ICollection<Image> images;
        private ICollection<UserCourse> usersCourses;

        public Course()
        {
            this.questions = new HashSet<Question>();
            this.images = new HashSet<Image>();
            this.usersCourses = new HashSet<UserCourse>();
        }


        public int Id { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        public bool Required { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public int ScoreToPass { get; set; }

        public virtual ICollection<Question> Questions
        {
            get
            {
                return this.questions;
            }
            set
            {
                this.questions = value;
            }
        }

        public virtual ICollection<Image> Images
        {
            get
            {
                return this.images;
            }
            set
            {
                this.images = value;
            }
        }

        public virtual ICollection<UserCourse> UsersCourses
        {
            get
            {
                return this.usersCourses;
            }
            set
            {
                this.usersCourses = value;
            }
        }


    }
}
