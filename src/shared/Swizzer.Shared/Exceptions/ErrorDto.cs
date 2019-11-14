using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Exceptions
{
    public class ErrorDto
    {
        public Exception Exception { get; set; }
        public string ErrorCode { get; set; }
    }
}
