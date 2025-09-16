using Shared.Errors;
using System.Net;

namespace Shared
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

    public class FailureWithErrors : IApiResponseWithErrors
    {
        public bool Success { get; init; } = false;
        public int StatusCode { get; init; }
        public string Message { get; init; }
        public Dictionary<string, List<ValidationErrorDetail>> Errors { get; init; }

        public FailureWithErrors(string message, Dictionary<string, List<ValidationErrorDetail>> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Message = message;
            Errors = errors;
            StatusCode = (int)statusCode;
        }
    }

    public class FailureMessageOnly : IApiResponseWithMessage
    {
        public bool Success { get; init; } = false;
        public int StatusCode { get; init; }
        public string Message { get; init; }

        public FailureMessageOnly(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Message = message;
            StatusCode = (int)statusCode;
        }
    }

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


