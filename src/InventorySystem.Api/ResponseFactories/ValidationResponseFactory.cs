using InventorySystem.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using Shared.Errors;
using System.Net;




namespace InventorySystem.ResponseFactories
{
    public class ValidationResponseFactory
    {


        public static IActionResult CustomValidationResponse(ActionContext
            actionContext)
        {


            var dictErrors = new Dictionary<string, List<ValidationErrorDetail>>();

            var errors = actionContext.ModelState.Where(pair => pair.Value.Errors.Any()).ToList();



            foreach (var item in errors)
            {

                dictErrors.Add(NormalizeFieldName(item.Key), item.Value.Errors.Select(e => new ValidationErrorDetail(e.ErrorMessage)).ToList());
            }


            var response = ApiResponseFactory.Failure("Validation Failed", dictErrors, HttpStatusCode.BadRequest);



            return new BadRequestObjectResult(response);



        }


        private static string NormalizeFieldName(string key)
        {
            // Removes "$." prefix (from JSONPath), and "dto." or "model." if present
            return key.Replace("$.", "")
                      .Replace("dto.", "")
                      .Replace("model.", "");
        }
    }
}
