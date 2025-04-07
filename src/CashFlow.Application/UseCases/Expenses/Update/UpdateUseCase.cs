using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public class UpdateUseCase : IUpdateUseCase
    {
        private readonly IExpenseUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateUseCase(
            IExpenseUpdateOnlyRepository Repository,
            IUnitOfWork UnitOfWork,
            IMapper Mapper)
        {
            _repository = Repository;
            _unitOfWork = UnitOfWork;
            _mapper = Mapper;
        }

        public async Task Execute(long id, RequestExpenseJson request)
        {
            Validate(request);

            var expense = await _repository.GetById(id);

            if (expense is null)
            {
                throw new NotFoundException(ResourceErrorMessage.NOT_FOUND_EXPENSE);
            }

            _mapper.Map(request,expense);

            _repository.Update(expense);

            await _unitOfWork.Commit();
        }

        public void Validate(RequestExpenseJson request)
        {
            var validator = new ExpenseValidator();
            var result = validator.Validate(request);

            if(result.IsValid == false){
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidateException(errorMessages);
            }
        }
    }
}
