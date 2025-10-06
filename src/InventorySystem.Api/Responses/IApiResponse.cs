using Shared.Errors;

namespace InventorySystem.Api.Responses
{
    public interface IApiResponse
    {
        public bool Success { get; init; }
        public int StatusCode { get; init; }
    }

    public interface IApiResponseWithData<T> : IApiResponse
    {
        public T? Data { get; init; }
    }

    public interface IApiResponseWithMessage : IApiResponse
    {
        public string? Message { get; init; }
    }

    public interface IApiResponseWithErrors : IApiResponseWithMessage
    {
        public Dictionary<string, List<ValidationErrorDetail>>? Errors { get; init; }
    }
}
