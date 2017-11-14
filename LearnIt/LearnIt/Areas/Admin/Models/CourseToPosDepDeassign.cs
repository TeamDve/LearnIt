using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnIt.Areas.Admin.Models
{
    public class CourseToPosDepDeassign
    {
        public string Possition { get; set; }

        public string CourseName { get; set; }

        public string Department { get; set; }

        public DateTime DueDate { get; set; }
    }
}