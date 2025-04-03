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
            var cashFlowException = context.Exception as CashFlowException;
            var errorResponse = new ResponseErrorJson(cashFlowException!.GetErrors());
            context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
            context.Result = new ObjectResult(new ResponseErrorJson(cashFlowException.Message));
        }
        private void ThrowUnknowException(ExceptionContext context)
        {
            var response = new ResponseErrorJson(ResourceErrorMessage.UNKNOW_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(response);
        }
    }
}
