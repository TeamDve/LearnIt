using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnIt.Areas.Admin.Models
{
    public class DepartPossitionAndCourseNames
    {
        public IEnumerable<Object> DepartmentList { get; set; }

        public IEnumerable<Object> CourseNameList { get; set; }

        public IEnumerable<Object> PossitionList { get; set; }
    }
}