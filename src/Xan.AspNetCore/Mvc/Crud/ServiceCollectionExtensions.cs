using Microsoft.Extensions.DependencyInjection;

namespace Xan.AspNetCore.Mvc.Crud;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCrud(this IServiceCollection services, Action<CrudOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        CrudOptions crudOptions = new(services);
        configureOptions(crudOptions);

        services.AddControllers(options =>
        {
            options.Conventions.Add(crudOptions.BuildConvention());
        });

        return services;
    }
}
