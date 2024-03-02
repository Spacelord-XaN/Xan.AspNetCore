using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Mvc.Crud.Core;

internal static class Utils
{
    public static string ControllerNameForEntity<TEntity>()
        where TEntity : class, ICrudEntity
    {
        Type entityType = typeof(TEntity);
        return entityType.Name.Replace("Entity", "");
    }

    public static string ViewName(string actionName)
    {
        ArgumentNullException.ThrowIfNull(actionName);

        return $"Crud{actionName}";
    }
}
