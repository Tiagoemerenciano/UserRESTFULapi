using CrosscuttingDomain;
using System;

namespace Crosscutting
{
    public class Response : IResponse
    {
        public string Message { get; internal set; }
        public int? ErrorCode { get; internal set; }

        public static Response CreateResponse(string message)
        {
            return new Response
            {
                Message = message
            };
        }

        public static Response CreateErrorResponse(string message, int errorCode)
        {
            return new Response
            {
                Message = message,
                ErrorCode = errorCode
            };
        }

        public static Response<T> CreateResponse<T>(T content)
        {
            var response = new Response<T>
            {
                Content = content,
                Message = "Success"
            };

            return response;
        }
    }

    public class Response<T> : Response, IResponse<T>
    {
        public T Content { get; set; }
    }
}
