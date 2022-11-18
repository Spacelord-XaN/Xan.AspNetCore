namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudItemModel<TEntity>
{
    public CrudItemModel(TEntity entity, bool canDelete)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        CanDelete = canDelete;
    }

    public TEntity Entity { get; }

    public bool CanDelete { get; }
}
