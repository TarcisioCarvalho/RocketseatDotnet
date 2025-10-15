using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowException)
        {
            HandleProjectException(context);
            return;
        }

        HandleUnknowException(context);
    }
        

    private void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = context.Exception as CashFlowException;
        var errorResponse = new ResponseErrorJson(cashFlowException.GetErrors());
        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);

        /* if (context.Exception is ErrorOnValidationException)
         {
             var ex = (ErrorOnValidationException)context.Exception;
             var errorResponse = new ResponseErrorJson(ex.Errors);
             context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
             context.Result = new BadRequestObjectResult(errorResponse);
             return;
         }
         if (context.Exception is NotFoundException)
         {
             var ex = (NotFoundException)context.Exception;
             var errorResponse = new ResponseErrorJson(ex.Message);
             context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
             context.Result = new NotFoundObjectResult(errorResponse);
             return;
         }
         var errorResponseGeneric = new ResponseErrorJson(context.Exception.Message);
         context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
         context.Result = new BadRequestObjectResult(errorResponseGeneric);*/
    }

    private void HandleUnknowException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorsMessages.UNKNOW_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
