using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        return new ResponseRegisterExpenseJson()
        {
            Title = request.Title,
            Amount = request.Amount,
            Time = request.Time
        };
    }


    public void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        var response = validator.Validate(request);

        if(response.IsValid == false)
        {
            var errorMessages = response.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidateException(errorMessages);
        }
    }
}

