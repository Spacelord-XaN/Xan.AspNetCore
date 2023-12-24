using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudRouter
{
    string ToCreate();

    string ToDelete(int id);

    string ToDisable(int id);

    string ToEdit(int id, string? origin = null);

    string ToEnable(int id);

    string ToList();

    string ToList(int pageIndex);

    string ToList(int? pageSize, int pageIndex);

    string GetUriByAction(string action, object? values = null);
}

public interface ICrudRouter<TEntity, TListParameter>
    : ICrudRouter
    where TEntity : class, ICrudEntity, new ()
    where TListParameter : ListParameter
{
    string ToList(TListParameter parameter);
}
