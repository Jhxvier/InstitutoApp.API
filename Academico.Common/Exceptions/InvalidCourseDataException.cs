using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Exceptions
{
    public class InvalidCourseDataException : Exception
    {
        public InvalidCourseDataException() { }
        public InvalidCourseDataException(string message) : base(message) { }
    }
}
