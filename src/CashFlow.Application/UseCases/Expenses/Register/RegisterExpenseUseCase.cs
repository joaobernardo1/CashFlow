using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;


public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unityOfWork;
    private readonly IMapper _mapper;

    public RegisterExpenseUseCase(
        IExpensesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
        _unityOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _repository.Add(entity);
        
        await _unityOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(entity);

    }


    public void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var response = validator.Validate(request);

        if(response.IsValid == false)
        {
            var errorMessages = response.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidateException(errorMessages);
        }
    }
}

