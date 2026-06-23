using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Exceptions
{
    public class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException() { }
        public DuplicateEnrollmentException(string message) : base(message) { }
    }
}
