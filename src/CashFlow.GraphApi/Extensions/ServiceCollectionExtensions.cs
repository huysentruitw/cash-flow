using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CashFlow.GraphApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Schema = CashFlow.GraphApi.Schema;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGraphApi(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Schema.Query>()
            .AddMutationType<Schema.Mutation>();

        services.AddSingleton<OutputTypesMapperResolver>(_ =>
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<OutputTypesProfile>()).CreateMapper();
            return () => mapper;
        });

        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IRequestInfo, RequestInfo>();
        return services;
    }
}
