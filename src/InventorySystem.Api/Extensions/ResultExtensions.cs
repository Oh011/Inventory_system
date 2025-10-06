using InventorySystem.Api.Responses;
using Shared.Results;
using System.Net;

namespace InventorySystem.Api.Extensions
{
    public static class ResultExtensions
    {

        private static HttpStatusCode MapError(ErrorType? error)
        {
            return error switch
            {
                ErrorType.NotFound => HttpStatusCode.NotFound,
                ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
                ErrorType.Forbidden => HttpStatusCode.Forbidden,
                ErrorType.Conflict => HttpStatusCode.Conflict,
                ErrorType.Validation => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }

        public static IApiResponse ToApiResponse<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                if (result.Value is not null)
                    return ApiResponseFactory.Success(result.Value);

                return ApiResponseFactory.Success(result.Message ?? "Operation completed successfully");
            }

            var statusCode = MapError(result.Error);

            if (result.ValidationErrors is not null && result.ValidationErrors.Any())
            {
                return ApiResponseFactory.Failure(
                    result.Message ?? "Validation failed",
                    result.ValidationErrors,
                    statusCode
                );
            }

            return ApiResponseFactory.Failure(
                result.Message ?? "Operation failed",
                statusCode
            );
        }

    }

}
