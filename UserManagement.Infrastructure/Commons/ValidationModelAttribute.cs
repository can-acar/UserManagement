using Microsoft.AspNetCore.Mvc.Filters;

namespace UserManagement.Infrastructure.Commons;

public class ValidationModelAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid) context.Result = new ValidationFailedResult(context.ModelState);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}