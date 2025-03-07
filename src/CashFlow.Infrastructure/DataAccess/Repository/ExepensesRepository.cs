using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repository
{
    internal class ExepensesRepository : IExpensesRepository
    {
        public void Add(Expense expense)
        {
            var dbContext = new CashFlowDbContext();

            dbContext.Expenses.Add(expense);

            dbContext.SaveChanges();
        }
    }
}
