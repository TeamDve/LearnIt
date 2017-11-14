using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnIt.Areas.Courses.Models
{
    public class QuestionInfo
    {
        public string Qstn { get; set; }

        public string[] Answers { get; set; }

        public string RightAnswer { get; set; }
    }
}