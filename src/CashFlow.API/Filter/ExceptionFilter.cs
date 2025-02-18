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
                ThrowUnknowExceptio(context);
            }
        }

        private void HandleException(ExceptionContext context)
        {
            if(context.Exception is ErrorOnValidateException)
            {
                var ex = (ErrorOnValidateException)context.Exception;
                var response = new ErrorOnValidateException(ex.ErrorMessages);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(response.ErrorMessages);

            }
        }
        private void ThrowUnknowExceptio(ExceptionContext context)
        {
            var response = new ErrorOnValidateException("An error occurred while registering the expense.");
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(response.Message);

        }
    }
}
