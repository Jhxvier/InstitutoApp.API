using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Exceptions
{
    public class InvalidEnrollmentDataException : Exception
    {
        public InvalidEnrollmentDataException() { }
        public InvalidEnrollmentDataException(string message) : base(message) { }
    }
}
