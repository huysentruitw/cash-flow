using AutoMapper;
using CashFlow.Query;
using CashFlow.Query.Abstractions.Repositories;
using CashFlow.Query.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQuery(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICodeRepository, CodeRepository>();
        services.AddScoped<IFinancialYearRepository, FinancialYearRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        services.AddSingleton<EntityMapperResolver>(_ =>
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityMapperProfile>()).CreateMapper();
            return () => mapper;
        });

        return services;
    }
}
