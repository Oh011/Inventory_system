using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net;




namespace InventorySystem.ResponseFactories
{
    public class ValidationResponseFactory
    {


        public static IActionResult CustomValidationResponse(ActionContext
            actionContext)
        {


            var dictErrors = new Dictionary<string, List<string>>();

            var errors = actionContext.ModelState.Where(pair => pair.Value.Errors.Any()).ToList();



            foreach (var item in errors)
            {

                dictErrors.Add(NormalizeFieldName(item.Key), item.Value.Errors.Select(e => e.ErrorMessage).ToList());
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
