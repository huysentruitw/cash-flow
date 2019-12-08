using CashFlow.Data;
using CashFlow.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IDataContext, DataContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
        return services;
    }
}
