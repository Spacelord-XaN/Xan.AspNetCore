namespace Xan.AspNetCore.Mvc.Abstractions;

public static class IEntityExtensions
{
    public static IEnumerable<int> Ids(this IEnumerable<IEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        return entities.Select(entity => entity.Id);
    }
}
