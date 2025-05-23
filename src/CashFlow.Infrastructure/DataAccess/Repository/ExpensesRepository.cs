﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repository
{
   
    internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpenseUpdateOnlyRepository
    {
        private readonly CashFlowDbContext _dbContext;
        public ExpensesRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }

        public async Task<List<Expense>> GetAll()
        {
            return await _dbContext.Expenses.AsNoTracking().ToListAsync();
        }

        async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        async Task<Expense?> IExpenseUpdateOnlyRepository.GetById(long id)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> Delete(long id)
        {
            var expense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);

            if (expense is null)
            {
                return false;
            }

            _dbContext.Expenses.Remove(expense);
            return true;
        }

        public void Update(Expense expense)
        {
            _dbContext.Update(expense);
        }

        public async Task<List<Expense>> FilterByMonth(DateOnly date)
        {

            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

            var daysInMounth = DateTime.DaysInMonth(year: date.Year, month: date.Month);

            var endDate = new DateTime(year: date.Year, month: date.Month,
                day: daysInMounth, hour: 23, minute: 59, second: 59);

            return await _dbContext.Expenses
               .AsNoTracking()
               .Where(expense => expense.Time >= startDate && expense.Time <= endDate)
               .OrderBy(expense => expense.Time)
               .ThenBy(expense => expense.Title)
               .ToListAsync();
        }
    }
}
