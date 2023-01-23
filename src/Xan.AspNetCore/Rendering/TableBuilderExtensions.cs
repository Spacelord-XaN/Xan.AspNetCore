using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Rendering;

public static class TableBuilderExtensions
{
	public static TableBuilder<CrudItemModel<TEntity>> CreatedAtColumn<TEntity>(this TableBuilder<CrudItemModel<TEntity>> builder)
		where TEntity : IHasTimestamps
	{
		ArgumentNullException.ThrowIfNull(builder);

		return builder.Column(c => c
			.AutoWidth()
			.Title(builder.Localizer[XanAspNetCoreTexts.CreatedAt])
			.ForTimeStamp(item => item.Entity.CreatedAt)
            );
    }

    public static TableBuilder<CrudItemModel<TEntity>> DeleteOrToggleStateLinkColumn<TEntity>(this TableBuilder<CrudItemModel<TEntity>> builder, ICrudRouter router)
        where TEntity : ICrudEntity
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(router);

        return builder.Column(c => c
            .AutoWidth()
            .ForLink(item => router.ToDeleteOrToggleState(item), item => builder.Localizer[XanAspNetCoreTexts.DeleteOrToggleState(item)])
            );
    }

    public static TableBuilder<TEntity> IdColumn<TEntity>(this TableBuilder<TEntity> builder)
        where TEntity : IEntity
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.IdColumn(item => item.Id);
    }

    public static TableBuilder<CrudItemModel<TEntity>> IdColumn<TEntity>(this TableBuilder<CrudItemModel<TEntity>> builder)
		where TEntity : IEntity
	{
		ArgumentNullException.ThrowIfNull(builder);

        return builder.IdColumn(item => item.Entity.Id);
    }

    public static TableBuilder<T> IdColumn<T>(this TableBuilder<T> builder, Func<T, int> getId)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getId);

        return builder.Column(c => c
            .AutoWidth()
            .Title(builder.Localizer[XanAspNetCoreTexts.Id])
            .For(item => getId(item))
            );
    }

    public static TableBuilder<CrudItemModel<TEntity>> EditLinkColumn<TEntity>(this TableBuilder<CrudItemModel<TEntity>> builder, ICrudRouter router)
        where TEntity : IEntity
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(router);

        return builder.Column(c => c
            .AutoWidth()
            .ForLink(item => router.ToEdit(item.Entity.Id), builder.Localizer[XanAspNetCoreTexts.Edit])
            );
    }

    public static TableBuilder<CrudItemModel<TEntity>> StateColumn<TEntity>(this TableBuilder<CrudItemModel<TEntity>> builder)
        where TEntity : ICrudEntity
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.Column(c => c
            .AutoWidth()
            .Title(builder.Localizer[XanAspNetCoreTexts.State])
            .For(item => builder.Localizer[XanAspNetCoreTexts.Singluar(item.Entity.State)])
            );
    }

    public static TableBuilder<CrudItemModel<TEntity>> UpdatedAtColumn<TEntity>(this TableBuilder<CrudItemModel<TEntity>> builder)
        where TEntity : IHasTimestamps
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.Column(c => c
            .AutoWidth()
            .Title(builder.Localizer[XanAspNetCoreTexts.UpdatedAt])
            .ForTimeStamp(item => item.Entity.UpdatedAt)
            );
    }
}
