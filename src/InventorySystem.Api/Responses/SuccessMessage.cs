using System.Net;

namespace InventorySystem.Api.Responses
{
    public class SuccessMessage : IApiResponseWithMessage
    {
        public bool Success { get; init; } = true;
        public int StatusCode { get; init; }
        public string? Message { get; init; }

        public SuccessMessage(string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Message = message;
            StatusCode = (int)statusCode;
        }
    }
}
