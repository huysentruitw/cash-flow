using CashFlow.Reporting;
using Microsoft.Extensions.DependencyInjection;
using jsreport.Client;
using jsreport.Shared;
using jsreport.Types;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReporting(this IServiceCollection services)
    {
        services.AddSingleton<IReportGenerator, ReportGenerator>();
        services.AddSingleton<IRenderService, ReportingService>(_ => new ReportingService(null));
        return services;
    }
}
