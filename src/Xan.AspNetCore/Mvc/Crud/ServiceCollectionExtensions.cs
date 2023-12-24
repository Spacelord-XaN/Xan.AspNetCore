using Microsoft.Extensions.DependencyInjection;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Mvc.Crud;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCrudServices<TEntity, TListParameter, TRouter, TService, TModelFactory>(this IServiceCollection services)
        where TEntity : class, ICrudEntity, new()
        where TListParameter : ListParameter
        where TRouter : class, ICrudRouter<TEntity, TListParameter>
        where TService : class, ICrudService<TEntity>
        where TModelFactory : class, ICrudModelFactory<TEntity, TListParameter>

    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .AddScoped<TRouter>()
            .AddScoped<ICrudRouter<TEntity, TListParameter>>(sp => sp.GetRequiredService<TRouter>())
            .AddScoped<TService>()
            .AddScoped<ICrudService<TEntity>>(sp => sp.GetRequiredService<TService>())
            .AddScoped<TModelFactory>()
            .AddScoped<ICrudModelFactory<TEntity, TListParameter>>(sp => sp.GetRequiredService<TModelFactory>())
            ;
    }
}
