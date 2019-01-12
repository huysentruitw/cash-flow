using AutoMapper;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using Microsoft.Extensions.DependencyInjection;
using CashFlow.GraphApi;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Schema = CashFlow.GraphApi.Schema;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGraphApi(this IServiceCollection services)
    {
        services.AddSingleton(provider => new GraphQLEngine()
            .WithFieldResolutionStrategy(FieldResolutionStrategy.Normal)
            .BuildSchema(typeof(SchemaDefinition<Schema.Query, Schema.Mutation>)));

        services.AddScoped<IDependencyInjector, Injector>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<Schema.Query>();
        services.AddScoped<Schema.Mutation>();

        services.AddScoped<DataLoaderContext>();

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
