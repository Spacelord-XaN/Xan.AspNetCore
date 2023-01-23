using System.Diagnostics;
using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Mvc.Crud;

public static class CrudRouterExtensions
{
    public static string ToDeleteOrToggleState<TEntity>(this ICrudRouter router, CrudItemModel<TEntity> crudItem)
        where TEntity : ICrudEntity
    {
        ArgumentNullException.ThrowIfNull(router);
        ArgumentNullException.ThrowIfNull(crudItem);

        if (crudItem.CanDelete)
        {
            return router.ToDelete(crudItem.Entity.Id);
        }

        return crudItem.Entity.State switch
        {
            ObjectState.Disabled => router.ToEnable(crudItem.Entity.Id),
            ObjectState.Enabled => router.ToDisable(crudItem.Entity.Id),
            _ => throw new UnreachableException(),
        };
    }
}
