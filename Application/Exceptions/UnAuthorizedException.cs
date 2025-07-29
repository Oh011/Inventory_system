namespace Application.Exceptions
{
    public class UnAuthorizedException : Exception
    {


        public UnAuthorizedException(string message = "Access denied. Your session has expired or the token " +
            "is invalid. Please log in again.") : base(message) { }
    }
}
