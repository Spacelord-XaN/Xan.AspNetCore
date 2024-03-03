using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.Mvc.Crud.CrudListModel;

public class Ctor
{
    [Theory]
    [AutoData]
    public void ShouldInitProperties(ListParameter parameter, CrudItemModel<TestEntity>[] items, int pageIndex, int pageSize, int totalPageCount, int totalItemCount, string listTitle, string createText)
    {
        //  Arrange
        IPaginatedList<CrudItemModel<TestEntity>> paginatedList = new PaginatedList<CrudItemModel<TestEntity>>(items, pageIndex, pageSize, totalPageCount, totalItemCount);
        CrudListModel<TestEntity, ListParameter, ICrudRouter>.CreateTableDelegate createTableDelegate = X.StrictFake<CrudListModel<TestEntity, ListParameter, ICrudRouter>.CreateTableDelegate>();
        ICrudRouter router = X.StrictFake<ICrudRouter>();

        //  Act
        CrudListModel<TestEntity, ListParameter, ICrudRouter> result = new(paginatedList, parameter, createTableDelegate, router, listTitle, createText);

        //  Assert
        using (new AssertionScope())
        {
            result.CreateText.Should().Be(createText);
            result.ListTitle.Should().Be(listTitle);
            result.PageIndex.Should().Be(pageIndex);
            result.PageSize.Should().Be(pageSize);
            result.Parameter.Should().Be(parameter);
            result.Router.Should().BeSameAs(router);
        }
    }
}
