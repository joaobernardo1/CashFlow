﻿using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            AddRepositories(services);
            AddDbContext(services, configuration);
        } 

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpensesRepository, ExepensesRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("Connection");
            var version = new Version(8, 0, 41);
            var serverVersion = new MySqlServerVersion(version);

            services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString,serverVersion));
        }
    }
}
