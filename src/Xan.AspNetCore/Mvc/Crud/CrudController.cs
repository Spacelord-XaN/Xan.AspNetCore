using FluentValidation;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudController<TEntity>
    : AbstractCrudController<TEntity, ListParameter>
    where TEntity : class, ICrudEntity, new()
{
    public CrudController(ICrudService<TEntity> service, ICrudRouter<TEntity> router, ICrudModelFactory<TEntity, ListParameter> modelFactory, IValidator<TEntity> validator)
        : base(service, router, modelFactory, validator)
    { }
}
