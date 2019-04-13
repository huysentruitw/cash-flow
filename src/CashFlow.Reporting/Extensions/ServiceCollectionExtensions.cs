using System;
using CashFlow.Reporting;
using jsreport.Client;
using jsreport.Shared;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReporting(this IServiceCollection services, Action<ReportingOptions> configureOptions)
    {
        var options = new ReportingOptions();
        configureOptions?.Invoke(options);
        options.Validate();
        services.AddSingleton<IReportGenerator, ReportGenerator>();
        services.AddSingleton<IRenderService, ReportingService>(_ => new ReportingService(options.JsReportServiceUri.ToString()));
        return services;
    }
}
