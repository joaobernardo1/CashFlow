using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;


public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository;
    private readonly IUnitOfWork _unityOfWork;

    public RegisterExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unityOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);

        var entity = new Expense
        {
            Amount = request.Amount,
            Time = request.Time,
            Description = request.Description,
            Title = request.Title,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
        };
        await _repository.Add(entity);
        await _unityOfWork.Commit();
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

