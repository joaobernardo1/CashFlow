using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Criptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repository;
using CashFlow.Infrastructure.Security.Criptography;
using CashFlow.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddToken(this IServiceCollection services, IConfiguration configuration)
        {
            var expirationTime = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAcessTokenGenerator>(config => new JwtTokenGenerator(signingKey!, expirationTime));
        }

        public static void AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            services.AddScoped<IPasswordEncrypter, Bcrypt>();
        } 

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
            services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpenseUpdateOnlyRepository, ExpensesRepository>();
            services.AddScoped<IUsersReadOnlyRepository, UsersRepository>();
            services.AddScoped<IUserWriteOnlyRepository, UsersRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            var version = new Version(8,0,41);
            var serverVersion = new MySqlServerVersion(version);
            services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
