using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using System.Security.Cryptography.X509Certificates;

namespace CashFlow.Application.UseCases.Expenses.Register;


public class RegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository;

    public RegisterExpenseUseCase(IExpensesRepository repository)
    {
        _repository = repository;
    }

    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        var entity = new Expense
        {
            Amount = request.Amount,
            Date = request.Time,
            Description = request.Description,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
            Title = request.Title
        };

        return new ResponseRegisterExpenseJson();
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

