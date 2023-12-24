using Microsoft.AspNetCore.Routing;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Mvc.Crud.Core;
using Xan.AspNetCore.Mvc.Routing;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Mvc.Crud;

public class CrudRouter<TEntity, TListParameter>
    : LinkRouter
    , ICrudRouter<TEntity, TListParameter>
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter, new()
{
    private const string _createAction = nameof(AbstractCrudController<TEntity, TListParameter>.Create);
    private const string _deleteAction = nameof(AbstractCrudController<TEntity, TListParameter>.Delete);
    private const string _disableAction = nameof(AbstractCrudController<TEntity, TListParameter>.Disable);
    private const string _editAction = nameof(AbstractCrudController<TEntity, TListParameter>.Edit);
    private const string _enableAction = nameof(AbstractCrudController<TEntity, TListParameter>.Enable);
    private const string _listAction = nameof(AbstractCrudController<TEntity, TListParameter>.List);

    private static readonly string _controllerName = Utils.ControllerName<TEntity>();

    public CrudRouter(LinkGenerator linkGenerator)
        : base(linkGenerator)
    { }

    public string ToCreate()
        => GetUriByAction(_createAction);

    public string ToDelete(int id)
        => GetUriByAction(_deleteAction, new { id });

    public string ToDisable(int id)
        => GetUriByAction(_disableAction, new { id });

    public string ToEnable(int id)
        => GetUriByAction(_enableAction, new { id });

    public string ToEdit(int id, string? origin = null)
        => GetUriByAction(_editAction, new { id, origin });

    public string ToList()
        => ToList(new TListParameter());

    public string ToList(int pageIndex)
        => ToList(new TListParameter { PageIndex = pageIndex });

    public string ToList(int? pageSize, int pageIndex)
        => ToList(new TListParameter { PageIndex = pageIndex, PageSize = pageSize });

    public string ToList(TListParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return GetUriByAction(_listAction, parameter);
    }

    public string GetUriByAction(string action, object? values = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        return GetUriByAction(_controllerName, action, values);
    }
}
