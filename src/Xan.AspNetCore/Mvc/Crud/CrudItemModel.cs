namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudItemModel<TEntity>(TEntity entity, bool canDelete)
{
    public TEntity Entity { get; } = entity ?? throw new ArgumentNullException(nameof(entity));

    public bool CanDelete { get; } = canDelete;
}
