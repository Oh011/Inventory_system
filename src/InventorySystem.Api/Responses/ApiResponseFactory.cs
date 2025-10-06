using Shared.Errors;
using System.Net;

namespace InventorySystem.Api.Responses
{
    public static class ApiResponseFactory
    {
        public static SuccessWithData<T> Success<T>(T? data, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(data, statusCode);

        public static SuccessMessage Success(string message, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(message, statusCode);

        public static FailureWithErrors Failure(string message, Dictionary<string, List<ValidationErrorDetail>> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(message, errors, statusCode);

        public static FailureMessageOnly Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(message, statusCode);
    }
}




//order.ReceiveItems(
//     request.Items.ToDictionary(i => i.ItemId, i => i.QuantityReceived)
// );