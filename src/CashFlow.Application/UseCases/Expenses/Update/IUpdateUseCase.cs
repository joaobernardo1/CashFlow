using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public interface IUpdateUseCase
    {
        Task Execute(long id, RequestExpenseJson request);
    }
}
