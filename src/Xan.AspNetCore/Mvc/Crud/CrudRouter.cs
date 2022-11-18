using Microsoft.AspNetCore.Routing;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Mvc.Crud.Core;
using Xan.AspNetCore.Mvc.Routing;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Mvc.Crud;

public class CrudRouter<TEntity>
    : LinkRouter
    , ICrudRouter<TEntity>
    where TEntity : class, ICrudEntity, new()
{
    public CrudRouter(LinkGenerator linkGenerator)
        : base(linkGenerator)
    { }

    public string ToCreate()
        => GetUriByAction(nameof(CrudController<TEntity>.Create));

    public string ToDelete(int id)
        => GetUriByAction(nameof(CrudController<TEntity>.Delete), new { id });

    public string ToDisable(int id)
        => GetUriByAction(nameof(CrudController<TEntity>.Disable), new { id });

    public string ToEnable(int id)
        => GetUriByAction(nameof(CrudController<TEntity>.Enable), new { id });

    public string ToEdit(int id)
        => GetUriByAction(nameof(CrudController<TEntity>.Edit), new { id });

    public string ToList()
        => ToList(new ListParameter());

    public string ToList(int pageIndex)
        => ToList(new ListParameter { PageIndex = pageIndex });

    public string ToList(int? pageSize, int pageIndex)
        => ToList(new ListParameter { PageIndex = pageIndex, PageSize = pageSize });

    public string ToList(ListParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return GetUriByAction(nameof(CrudController<TEntity>.List), parameter);
    }

    public string GetUriByAction(string action, object? values = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        return GetUriByAction(Utils.ControllerName<TEntity>(), action, values);
    }
}
