using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudModelFactoryTests;

public class TestCrudModelFactory(ICrudRouter router)
    : AbstractCrudModelFactory<TestEntity, ListParameter, ICrudRouter>(router)
{
    public static string CreateTitleString { get; } = Guid.NewGuid().ToString();
    public static string EditTitleString { get; } = Guid.NewGuid().ToString();
    public static string ListTitleString { get; } = Guid.NewGuid().ToString();

    protected override string CreateTitle => CreateTitleString;

    protected override string EditTitle => EditTitleString;

    protected override string ListTitle => ListTitleString;

    protected override Task<IHtmlContent> CreateEditorAsync(ViewContext viewContext, TestEntity entity)
    {
        throw new NotImplementedException();
    }

    protected override Task<IHtmlContent> CreateTableAsync(ViewContext viewContext, IPaginatedList<CrudItemModel<TestEntity>> model)
    {
        throw new NotImplementedException();
    }
}
