using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearnIt.Areas.Admin.Models
{
    public class CourseToPosDepDeassign
    {
        [Required(ErrorMessage = "The position is required")]
        public string Possition { get; set; }

        [Required(ErrorMessage = "The course name is required")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "The department is required")]
        public string Department { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "The date is required")]
        public DateTime DueDate { get; set; }
    }
}