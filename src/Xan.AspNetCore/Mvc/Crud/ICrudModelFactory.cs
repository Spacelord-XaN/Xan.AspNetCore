using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudModelFactory<TEntity, TListParameter>
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter
{
    Task<ICrudModel> CreateModelAsync(TEntity entity);

    Task<ICrudModel> EditModelAsync(TEntity entity);

    Task<ICrudListModel> ListModelAsync(IPaginatedList<CrudItemModel<TEntity>> items, TListParameter parameter);
}
