using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        return new ResponseRegisterExpenseJson();
    }


    public void Validate(RequestRegisterExpenseJson request)
    {
        var TitleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        if (TitleIsEmpty)
        {
            throw new ArgumentException("The title is required.");
        }

        if(request.Amount < 0)
        {
            throw new ArgumentException("The amount most be greater than zero.");
        }

        var DateIsValid = DateTime.Compare(request.Time, DateTime.UtcNow);
        if(DateIsValid > 0)
        {
            throw new ArgumentException("Expanses cannot be for the future");
        }

        var EnumIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);

        if(EnumIsValid == false)
        {
            throw new ArgumentException("The payment type is invalid");
        }
    }
}

