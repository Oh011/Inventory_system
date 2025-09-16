using FluentValidation;
using MediatR;
using Shared.Errors;
using ValidationException = Application.Exceptions.ValidationException;

namespace Application.Common.Behaviors
{

    //MediatR Pipeline Behavior that automatically validates all commands and queries
    //using FluentValidation before your handler runs.
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {


        //List of validators

        private readonly IEnumerable<IValidator<TRequest>> _validators;


        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
        {

            this._validators = validator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {



            if (_validators.Any())
            {


                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(

                       _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                    );


                var errors = validationResults.SelectMany(result => result.Errors)
                    .Where(errors => errors != null).ToList();



                if (errors.Count > 0)
                {
                    var errorDict = errors
                  .GroupBy(f => f.PropertyName)
                  .ToDictionary(
                      g => g.Key,
                      g => g.Select(e => new ValidationErrorDetail(e.ErrorMessage)).ToList()
                  );
                    throw new ValidationException(errorDict);
                }


            }
            return await next();
        }
    }
}


//✅ What is This Class ?
//ValidationBehavior<TRequest, TResponse> :
//It’s a generic behavior that intercepts every MediatR request (IRequest<TResponse>).

//TRequest: the command/ query being sent
//TResponse: the response type expected




//You send a command (e.g. CreateUserCommand) to MediatR.Send(...).

//MediatR goes through the pipeline behaviors before it reaches your handler.

//Your custom ValidationBehavior<TRequest, TResponse> intercepts the request and:

//Finds all validators for that TRequest

//Runs them

//If validation errors exist ➝ throws FluentValidation.ValidationException

//Your global exception middleware catches that and maps it to your ApiResponse<T>.