using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce3APIs_V01.Extensions
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Put || context.HttpContext.Request.Method == HttpMethods.Post)
            {
                // If the model state is invalid, return a bad request with validation errors
                if (!context.ModelState.IsValid)
                {
                    var errors = context.ModelState
                                        .Where(x => x.Value.Errors.Count > 0)
                                        .ToDictionary(
                                            kvp => kvp.Key,
                                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                        );

                    context.Result = new BadRequestObjectResult(errors);
                }
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No need to do anything here
        }
    }
}
