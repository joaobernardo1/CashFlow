using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repository
{
    internal class UsersRepository : IUsersReadOnlyRepository

    {
        private readonly CashFlowDbContext _dbContext;
        public UsersRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> ExistsEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
        }
    }
}
