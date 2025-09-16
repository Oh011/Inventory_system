using Shared.Errors;

namespace Application.Exceptions
{
    public class ValidationException : BadRequestException
    {



        public Dictionary<string, List<ValidationErrorDetail>> Errors { get; }

        public ValidationException(Dictionary<string, List<ValidationErrorDetail>> errors, string? message = "Validation failed")
            : base(message)
        {
            Errors = errors;
        }


        public void AddValidations(string key, ValidationErrorDetail value)
        {

            if (Errors.ContainsKey(key))
                Errors[key].Add(value);


            else
            {
                Errors[key] = new List<ValidationErrorDetail>();
                Errors[key].Add(value);
            }
        }




        public ValidationException(Dictionary<string, List<ValidationErrorDetail>> errors) : base("Validation failed")
        {

            this.Errors = errors;
        }


    }
}
