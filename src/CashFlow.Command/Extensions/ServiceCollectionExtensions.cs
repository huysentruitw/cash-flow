using System;
using System.Collections.Generic;
using System.Linq;
using CashFlow.Command.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommand(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>(provider => new Mediator(provider.GetService));
        services.RegisterCommandHandlersInAssembly();
        return services;
    }

    private static void RegisterCommandHandlersInAssembly(this IServiceCollection services)
    {
        IEnumerable<(Type interfaceType, Type implementationType)> handlers = typeof(ServiceCollectionExtensions).Assembly.GetTypes()
            .Where(x => x.GetInterface(typeof(IRequestHandler<,>).FullName) != null && x.IsClass && !x.IsAbstract)
            .Select(x => (x.GetInterface(typeof(IRequestHandler<,>).FullName), x));
        foreach ((Type interfaceType, Type implementationType) in handlers)
            services.AddScoped(interfaceType, implementationType);
    }
}
