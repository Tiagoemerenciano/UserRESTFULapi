using CrosscuttingDomain;

namespace Crosscutting
{
    public class Response : IResponse
    {
        public string Message { get; internal set; }

        public static Response CreateResponse(string message)
        {
            return new Response
            {
                Message = message
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

    public class ErrorResponse : IErrorResponse
    {
        public string Message { get; internal set; }
        public int ErrorCode { get; internal set; }

        public static ErrorResponse CreateErrorResponse(string message, int errorCode)
        {
            return new ErrorResponse
            {
                Message = message,
                ErrorCode = errorCode
            };
        }
    }

    public class Response<T> : Response, IResponse<T>
    {
        public T Content { get; set; }
    }
}
