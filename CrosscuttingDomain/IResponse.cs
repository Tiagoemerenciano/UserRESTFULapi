namespace CrosscuttingDomain
{
    public interface IResponse
    {
        string Message { get; }
    }

    public interface IErrorResponse
    {
        string Message { get; }
        int ErrorCode { get; }
    }

    public interface IResponse<T> : IResponse
    {
        T Content { get; set; }
    }
}
