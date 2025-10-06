using System.Net;

namespace InventorySystem.Api.Responses
{
    public class SuccessWithData<T> : IApiResponseWithData<T>
    {
        public bool Success { get; init; } = true;
        public int StatusCode { get; init; }
        public T Data { get; init; }

        public SuccessWithData(T? data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Data = data;
            StatusCode = (int)statusCode;
        }
    }

}
