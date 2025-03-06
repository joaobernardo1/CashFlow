using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repository
{
    internal class ExepensesRepository : IExpensesRepository
    {
        public void Add(Expense expense)
        {
            var dbContext = new CashFlowDbContext();

            dbContext.Add(expense);

            dbContext.SaveChanges();
        }
    }
}
