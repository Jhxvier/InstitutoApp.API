using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Exceptions
{
    public class DuplicateStudentEmailException : Exception
    {
        public DuplicateStudentEmailException() { }
        public DuplicateStudentEmailException(string message) : base(message) { }
    }
}
