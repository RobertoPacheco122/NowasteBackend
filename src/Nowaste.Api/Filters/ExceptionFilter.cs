using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nowaste.Communication.Responses;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Api.Filters;
public class ExceptionFilter : IExceptionFilter {
    public void OnException(ExceptionContext context) {
        if(context.Exception is NowasteException) {
            HandleProjectException(context);
        } else {
            ThrowUnknowError(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context) {
        var nowasteException = (NowasteException)context.Exception;
        var errorResponse = new ResponseErrorJson(nowasteException.GetErrors());

        context.HttpContext.Response.StatusCode = nowasteException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private static void ThrowUnknowError(ExceptionContext context) {
        var errorResponse = new ResponseErrorJson("Unknown error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
