using System;

namespace CrosscuttingDomain
{
    public interface IResponse
    {
        string Message { get; }
        public int? ErrorCode { get; }
    }

    public interface IResponse<T> : IResponse
    {
        T Content { get; set; }
    }
}
