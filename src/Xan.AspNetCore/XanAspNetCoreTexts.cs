using System.Diagnostics;
using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore;

public static class XanAspNetCoreTexts
{
    public const string Prefix = "Xan_AspNetCore_";

    public const string AllStates = $"{Prefix}{nameof(AllStates)}";
    public const string CreatedAt = $"{Prefix}{nameof(CreatedAt)}";
    public const string Cancel = $"{Prefix}{nameof(Cancel)}";
    public const string Delete = $"{Prefix}{nameof(Delete)}";
    public const string Disable = $"{Prefix}{nameof(Disable)}";
    public const string Disabled = $"{Prefix}{nameof(Disabled)}";
    public const string Edit = $"{Prefix}{nameof(Edit)}";
    public const string Enable = $"{Prefix}{nameof(Enable)}";
    public const string Enabled = $"{Prefix}{nameof(Enabled)}";
    public const string Id = $"{Prefix}{nameof(Id)}";
    public const string Save = $"{Prefix}{nameof(Save)}";
    public const string State = $"{Prefix}{nameof(State)}";
    public const string UpdatedAt = $"{Prefix}{nameof(UpdatedAt)}";

    public static string DeleteOrToggleState<TEntity>(CrudItemModel<TEntity> crudItem)
        where TEntity : ICrudEntity
    {
        ArgumentNullException.ThrowIfNull(crudItem);
        
        if (crudItem.CanDelete)
        {
            return Delete; ;
        }

        return crudItem.Entity.State switch
        {
            ObjectState.Disabled => Enable,
            ObjectState.Enabled => Disable,
            _ => throw new UnreachableException(),
        };
    }

    public static string Singluar(ObjectState? objectState)
        => objectState switch
        {
            null => AllStates,
            ObjectState.Disabled => Disabled,
            ObjectState.Enabled => Enabled,
            _ => throw new UnreachableException(),
        };
}
