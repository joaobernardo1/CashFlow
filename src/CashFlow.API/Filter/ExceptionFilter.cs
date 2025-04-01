using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is CashFlowException)
            {
                HandleException(context);
            }
            else
            {
                ThrowUnknowException(context);
            }
        }

        private void HandleException(ExceptionContext context)
        {
            if(context.Exception is ErrorOnValidateException)
            {
                var ex = (ErrorOnValidateException)context.Exception;
                var response = new ResponseErrorJson(ex.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(response);
            }
            else
            {
                var errorResponse = new ResponseErrorJson(context.Exception.Message);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }
        private void ThrowUnknowException(ExceptionContext context)
        {
            var response = new ResponseErrorJson(ResourceErrorMessage.UNKNOW_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(response);
        }
    }
}
