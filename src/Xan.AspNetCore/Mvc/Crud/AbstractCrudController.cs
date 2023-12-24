using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.EntityFrameworkCore;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Mvc.Crud.Core;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public abstract class AbstractCrudController<TEntity, TListParameter>
    : AbstractXanController
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter
{
    public abstract Task<IActionResult> Create();

    [HttpPost]
    public abstract Task<IActionResult> Create(TEntity entity);

    public abstract Task<IActionResult> Delete(int id);

    public abstract Task<IActionResult> Disable(int id);

    public abstract Task<IActionResult> Enable(int id);

    public abstract Task<IActionResult> Edit(int id);

    [HttpPost]
    public abstract Task<IActionResult> Edit(TEntity entity, string? origin);

    public abstract Task<IActionResult> List(TListParameter parameter);
}

public abstract class AbstractCrudController<TEntity, TListParameter, TRouter, TService>
    : AbstractCrudController<TEntity, TListParameter>
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter
    where TRouter : ICrudRouter<TEntity, TListParameter>
    where TService : ICrudService<TEntity>
{
    private readonly ICrudModelFactory<TEntity, TListParameter> _modelFactory;
    private readonly IValidator<TEntity> _validator;

    public AbstractCrudController(TService service, TRouter router, ICrudModelFactory<TEntity, TListParameter> modelFactory, IValidator<TEntity> validator)
    {
        Service = service ?? throw new ArgumentNullException(nameof(service));
        Router = router ?? throw new ArgumentNullException(nameof(router));
        _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
    
    protected TRouter Router { get; }

    protected TService Service { get; }

    public override async Task<IActionResult> Create()
    {
        TEntity entity = await Service.CreateNewAsync();
        ICrudModel model = await _modelFactory.CreateModelAsync(entity);
        return View(Utils.ViewName(nameof(Create)), model);
    }

    [HttpPost]
    public override async Task<IActionResult> Create(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        SetValidationResult(_validator.Validate(entity));

        if (ModelState.IsValid)
        {
            await Service.CreateAsync(entity);
            return Redirect(Router.ToList());
        }

        ICrudModel model = await _modelFactory.CreateModelAsync(entity);
        return View(Utils.ViewName(nameof(Create)), model);
    }

    public override async Task<IActionResult> Delete(int id)
    {
        await Service.DeleteAsync(id);
        return RedirectToReferer();
    }

    public override async Task<IActionResult> Disable(int id)
    {
        await Service.DisableAsync(id);
        return RedirectToReferer();
    }

    public override async Task<IActionResult> Enable(int id)
    {
        await Service.EnableAsync(id);
        return RedirectToReferer();
    }

    public override async Task<IActionResult> Edit(int id)
    {
        TEntity entity = await Service.GetAsync(id);
        ICrudModel model = await _modelFactory.EditModelAsync(entity);
        return View(Utils.ViewName(nameof(Edit)), model);
    }

    [HttpPost]
    public override async Task<IActionResult> Edit(TEntity entity, string? origin)
    {
        ArgumentNullException.ThrowIfNull(entity);

        SetValidationResult(_validator.Validate(entity));

        if (ModelState.IsValid)
        {
            await Service.UpdateAsync(entity);
            return RedirectToOrigin(entity, origin);
        }

        ICrudModel model = await _modelFactory.EditModelAsync(entity);
        return View(Utils.ViewName(nameof(Edit)), model);
    }

    public override async Task<IActionResult> List(TListParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);
        ArgumentNullException.ThrowIfNull(parameter.PageSize);        

        IPaginatedList<CrudItemModel<TEntity>> items = await GetMany(parameter)
            .AsPaginatedAsync(parameter.PageSize.Value, parameter.PageIndex, (Func<TEntity, Task<CrudItemModel<TEntity>>>)(async entity =>
            {
                bool canDelete = await Service.CanDeleteAsync(entity);
                CrudItemModel<TEntity> item = new(entity, canDelete);
                return item;
            })); ;
        ICrudListModel model = await _modelFactory.ListModelAsync(items, parameter);
        return View(Utils.ViewName(nameof(List)), model);
    }

    protected virtual IActionResult RedirectToOrigin(TEntity entity, string? origin)
        => Redirect(Router.ToList());

    protected abstract IQueryable<TEntity> GetMany(TListParameter parameter);
}
