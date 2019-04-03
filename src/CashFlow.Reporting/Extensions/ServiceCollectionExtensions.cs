using CashFlow.Reporting;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReporting(this IServiceCollection services)
    {
        services.AddSingleton<IReportGenerator, ReportGenerator>();
        return services;
    }
}
