using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Toy.EfCore.Learning.Exceptions;

namespace Toy.EfCore.Learning.Controllers.Filters;

public class GlobalExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;

        if (exception is IBadRequestException)
        {
            var body = new { exception.Message };
            context.Result = new JsonResult(body)
            {
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }
        else if (exception is IInternalServerException)
        {
            var body = new { exception.Message };
            context.Result = new JsonResult(body)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }

        return Task.CompletedTask;
    }
}