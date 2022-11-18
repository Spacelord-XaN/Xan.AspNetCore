using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Mvc.Crud.Core;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudOptions
{
    private readonly List<ControllerModel> _controllerModels = new ();
    private readonly IServiceCollection _services;

    public CrudOptions(IServiceCollection services)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public void AddController<TEntity, TCrudService, TModelFactory>(bool authorize)
        where TEntity : class, ICrudEntity, new()
        where TCrudService : AbstractCrudService<TEntity>
        where TModelFactory : AbstractCrudModelFactory<TEntity>
    {
        _services.AddSingleton<ICrudRouter<TEntity>, CrudRouter<TEntity>>();
        _services.AddScoped<TCrudService>();
        _services.AddScoped<ICrudService<TEntity>>(sp => sp.GetRequiredService<TCrudService>());
        _services.AddScoped<TModelFactory>();
        _services.AddScoped<ICrudModelFactory<TEntity, ListParameter>>(sp => sp.GetRequiredService<TModelFactory>());

        ControllerModel controllerModel = ApplicationModelProvider.CreateAndConfigureControllerModel(typeof(CrudController<TEntity>), Utils.ControllerName<TEntity>(), authorize);
        _controllerModels.Add(controllerModel);
    }

    internal CrudConvention BuildConvention()
        => new (_controllerModels);
}
