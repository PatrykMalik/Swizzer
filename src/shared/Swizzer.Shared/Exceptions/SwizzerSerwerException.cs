using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Swizzer.Shared.Exceptions
{
    public class SwizzerSerwerException : Exception
    {
        public string ErrorCode { get; }
        public SwizzerSerwerException(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public SwizzerSerwerException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public SwizzerSerwerException(string errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;

        }

        protected SwizzerSerwerException(string errorCode, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ErrorCode = errorCode;
        }


    }
}
