using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
    private readonly ICrudService<TEntity> _service;
    private readonly ICrudRouter<TEntity> _router;
    private readonly ICrudModelFactory<TEntity, TListParameter> _modelFactory;
    private readonly IValidator<TEntity> _validator;

    public AbstractCrudController(ICrudService<TEntity> service, ICrudRouter<TEntity> router, ICrudModelFactory<TEntity, TListParameter> modelFactory, IValidator<TEntity> validator)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _router = router ?? throw new ArgumentNullException(nameof(router));
        _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public virtual async Task<IActionResult> Create()
    {
        TEntity entity = await _service.CreateNewAsync();
        ICrudModel model = await _modelFactory.CreateModelAsync(entity);
        return View(Utils.ViewName(nameof(Create)), model);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        SetValidationResult(_validator.Validate(entity));

        if (ModelState.IsValid)
        {
            await _service.CreateAsync(entity);
            return Redirect(_router.ToList());
        }

        ICrudModel model = await _modelFactory.CreateModelAsync(entity);
        return View(Utils.ViewName(nameof(Create)), model);
    }

    public virtual async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToReferer();
    }

    public virtual async Task<IActionResult> Disable(int id)
    {
        await _service.DisableAsync(id);
        return RedirectToReferer();
    }

    public virtual async Task<IActionResult> Enable(int id)
    {
        await _service.EnableAsync(id);
        return RedirectToReferer();
    }

    public virtual async Task<IActionResult> Edit(int id)
    {
        TEntity entity = await _service.GetAsync(id);
        ICrudModel model = await _modelFactory.EditModelAsync(entity);
        return View(Utils.ViewName(nameof(Edit)), model);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Edit(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        SetValidationResult(_validator.Validate(entity));

        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(entity);
            return Redirect(_router.ToList());
        }

        ICrudModel model = await _modelFactory.EditModelAsync(entity);
        return View(Utils.ViewName(nameof(Edit)), model);
    }

    public virtual async Task<IActionResult> List(TListParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);
        ArgumentNullException.ThrowIfNull(parameter.PageSize);

        IPaginatedList<CrudItemModel<TEntity>> items = await _service.GetManyAsync(parameter.PageSize.Value, parameter.PageIndex, parameter.SearchString, parameter.State);
        ICrudListModel model = await _modelFactory.ListModelAsync(items, parameter);
        return View(Utils.ViewName(nameof(List)), model);
    }
}
