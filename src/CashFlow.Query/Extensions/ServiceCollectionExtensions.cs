using AutoMapper;
using CashFlow.Query;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQuery(this IServiceCollection services)
    {
        services.AddSingleton<EntityMapperResolver>(_ =>
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityMapperProfile>()).CreateMapper();
            return () => mapper;
        });

        return services;
    }
}
