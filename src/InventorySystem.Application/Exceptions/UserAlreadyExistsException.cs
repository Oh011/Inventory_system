namespace Application.Exceptions
{
    public class UserAlreadyExistsException : ConflictException
    {

        public UserAlreadyExistsException(string parmater)
       : base($"A user with the email '{parmater}' already exists.") { }
    }
}
