﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnIt.Areas.Admin.Models
{
    public class UserNameAndProjectNameModel
    {
        public IEnumerable<Object> UsernameList { get; set; }

        public IEnumerable<Object> CourseNameList { get; set; }
    }
}