using Shared.Errors;

namespace Shared.Results
{





    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public Dictionary<string, List<ValidationErrorDetail>>? ValidationErrors { get; }
        public T? Value { get; }
        public ErrorType? Error { get; }

        private Result(bool isSuccess, T? value, string? message,
                       Dictionary<string, List<ValidationErrorDetail>>? validationErrors,
                       ErrorType? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Message = message;
            ValidationErrors = validationErrors;
            Error = error;
        }

        public static Result<T> Success(T value, string? message = null)
            => new(true, value, message, null, null);

        //  Success with no data (used in commands)
        public static Result<Unit> Success(string? message = null)
            => new(true, Unit.Value, message, null, null);

        //  Failure returning the same T type (safe for queries)
        public static Result<T> Failure(string message, ErrorType error = ErrorType.Internal)
            => new(false, default, message, null, error);

        public static Result<T> Failure(string message,
                                        Dictionary<string, List<ValidationErrorDetail>> validationErrors,
                                        ErrorType error = ErrorType.Validation)
            => new(false, default, message, validationErrors, error);

        //  Failure with no data (safe for commands)
        public static Result<Unit> FailureUnit(string message, ErrorType error = ErrorType.Internal)
            => new(false, default, message, null, error);

        public static Result<Unit> FailureUnit(string message,
                                               Dictionary<string, List<ValidationErrorDetail>> validationErrors,
                                               ErrorType error = ErrorType.Validation)
            => new(false, default, message, validationErrors, error);
    }


    public sealed class Unit
    {
        public static readonly Unit Value = new Unit();
        private Unit() { }
    }


}