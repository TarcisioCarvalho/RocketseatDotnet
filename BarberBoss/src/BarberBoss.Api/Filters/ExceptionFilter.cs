using BarberBoss.Communication.Responses;
using BarberBoss.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberBoss.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is BarberBossException)
        {
            HandleProjectException(context);
            return;
        }
        HandleUnknowException(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var barberBossException = context.Exception as BarberBossException;
        var errorResponse = new ResponseErrorJson(barberBossException.GetErrors().ToList());
        context.HttpContext.Response.StatusCode = barberBossException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void HandleUnknowException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson("An unexpected error occurred.");
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
