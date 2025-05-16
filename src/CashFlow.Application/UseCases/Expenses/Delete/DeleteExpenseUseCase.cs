
using AutoMapper;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUseCase : IDeleteExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unityOfWork;

        public DeleteExpenseUseCase(
            IExpensesWriteOnlyRepository repository,
            IUnitOfWork unityOfWork)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
        }

        public async Task Execute(long id)
        {
            var result = await _repository.Delete(id);
            if(result is false)
            {
                throw new NotFoundException(ResourceErrorMessage.NOT_FOUND_EXPENSE);
            }

            await _unityOfWork.Commit();
            
        }
    }
}
