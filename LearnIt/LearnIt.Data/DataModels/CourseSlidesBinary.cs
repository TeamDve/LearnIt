using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnIt.Data.DataModels
{
    public class CourseSlidesBinary
    {
        public int Order { get; set; }
        public byte[] ImageBinary { get; set; }
    }
}
