using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Exceptions
{
    public class InvalidStudentDataException : Exception
    {
        public InvalidStudentDataException() { }
        public InvalidStudentDataException(string message) : base(message) { }
    }
}
