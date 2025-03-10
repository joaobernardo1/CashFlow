using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repository
{
   
    internal class ExepensesRepository : IExpensesRepository
    {
        private readonly CashFlowDbContext _dbContext;
        public ExepensesRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Expense expense)
        {

            _dbContext.Expenses.Add(expense);

            _dbContext.SaveChanges();
        }
    }
}
