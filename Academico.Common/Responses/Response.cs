using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico.Common.Responses
{
    public class Response <T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}
