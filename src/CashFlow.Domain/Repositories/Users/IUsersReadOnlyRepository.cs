namespace CashFlow.Domain.Repositories.Users
{
    public interface IUsersReadOnlyRepository
    {
        public Task<bool> ExistsEmail(string email);
    }
}
