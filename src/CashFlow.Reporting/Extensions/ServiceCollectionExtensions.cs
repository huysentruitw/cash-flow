using CashFlow.Reporting.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using HandlebarsDotNet;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReporting(this IServiceCollection services)
    {
        services.AddSingleton<IPdfGenerator, PdfGenerator>();
        services.AddSingleton<IHandlebars>(_ => Handlebars.Create());
        services.AddSingleton<ITools, PdfTools>();
        services.AddSingleton<IConverter, SynchronizedConverter>();
        services.AddScoped<IReportService, ReportService>();
        return services;
    }
}
