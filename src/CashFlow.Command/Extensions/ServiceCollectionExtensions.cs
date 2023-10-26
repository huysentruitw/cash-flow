using CashFlow.Command.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommand(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions));
        });

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICodeRepository, CodeRepository>();
        services.AddScoped<IFinancialYearRepository, FinancialYearRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
