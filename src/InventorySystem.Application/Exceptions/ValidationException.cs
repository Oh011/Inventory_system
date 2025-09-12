namespace Application.Exceptions
{
    public class ValidationException : BadRequestException
    {

        public Dictionary<string, List<string>> errors { get; }



        public ValidationException(string? msg = "Validation failed") : base(msg)
        {


            errors = new();

        }


        public void AddValidations(string key, string value)
        {

            if (errors.ContainsKey(key))
                errors[key].Add(value);


            else
            {
                errors[key] = new List<string>();
                errors[key].Add(value);
            }
        }


        public ValidationException(Dictionary<string, List<string>> errors, string? msg = "Validation failed") : base(msg)
        {

            this.errors = errors;
        }

        public ValidationException(Dictionary<string, List<string>> errors) : base("Validation failed")
        {

            this.errors = errors;
        }


    }
}
