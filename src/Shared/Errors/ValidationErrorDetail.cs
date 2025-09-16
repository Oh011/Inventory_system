namespace Shared.Errors
{
    // Base class for all validation error details
    public class ValidationErrorDetail
    {
        public string Message { get; set; }

        public ValidationErrorDetail(string message)
        {
            Message = message;
        }
    }



}
