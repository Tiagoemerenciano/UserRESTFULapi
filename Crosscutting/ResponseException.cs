using System;
using System.Collections.Generic;
using System.Text;

namespace Crosscutting
{
    public class ResponseException : Exception
    {
        public int Code { get; private set; }

        public ResponseException(string message, int code): base(message)
        {
            Code = code;
        }
    }
}
