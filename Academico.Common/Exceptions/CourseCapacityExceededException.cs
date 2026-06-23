using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Exceptions
{
    public class CourseCapacityExceededException : Exception
    {
        public CourseCapacityExceededException() { }
        public CourseCapacityExceededException(string message) : base(message) { }
    }
}
