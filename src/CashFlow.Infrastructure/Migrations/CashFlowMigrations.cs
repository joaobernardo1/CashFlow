using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure.Migrations
{
    public static class CashFlowMigrations
    {
        public async static Task MigrateDatabase(IServiceProvider serviceprovider)
        {
            var dbContext = serviceprovider.GetRequiredService<CashFlowDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
