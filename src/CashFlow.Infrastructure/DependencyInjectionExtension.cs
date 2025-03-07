using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            
            services.AddScoped<IExpensesRepository, ExepensesRepository>();
        } 
    }
}
