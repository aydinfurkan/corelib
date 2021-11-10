using System.Linq;
using CoreLib.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreLib.Validations
{
    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var (key, value) = context.ModelState.FirstOrDefault();
                throw new BadRequest(4000, key, value.AttemptedValue);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}