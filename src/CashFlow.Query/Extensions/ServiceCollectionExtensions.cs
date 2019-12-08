using CashFlow.Query.Abstractions.Repositories;
using CashFlow.Query.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQuery(this IServiceCollection services)
    {
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ICodeBalanceRepository, CodeBalanceRepository>();
        services.AddTransient<ICodeRepository, CodeRepository>();
        services.AddTransient<IFinancialYearRepository, FinancialYearRepository>();
        services.AddTransient<ISupplierRepository, SupplierRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        return services;
    }
}
