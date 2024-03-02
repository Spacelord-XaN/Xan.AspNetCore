using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class List
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldGetItemsAndReturnView(ListParameter parameter, CrudItemModel<TestEntity>[] items, int pageIndex, int pageSize, int totalPageCount, int totalItemCount, string listTitle, string createText)
    {
        //  Arrange
        IPaginatedList<CrudItemModel<TestEntity>> paginatedList = new PaginatedList<CrudItemModel<TestEntity>>(items, pageIndex, pageSize, totalPageCount, totalItemCount);
        CrudListModel<TestEntity, ListParameter, ICrudRouter>.CreateTableDelegate createTableDelegate = X.StrictFake<CrudListModel<TestEntity, ListParameter, ICrudRouter>.CreateTableDelegate>();
        CrudListModel<TestEntity, ListParameter, ICrudRouter> model = new (paginatedList, parameter, createTableDelegate, Router, listTitle, createText);

        A.CallTo(() => Service.GetManyAsync(parameter))
            .Returns(paginatedList);
        A.CallTo(() => ModelFactory.ListModelAsync(paginatedList, parameter))
            .Returns(model);

        //  Act
        IActionResult result = await Sut.List(parameter);

        //  Assert
        using (new AssertionScope())
        {
            ViewResult view = result.Should().BeOfType<ViewResult>().Subject;
            view.ViewName.Should().Be("CrudList");
            view.ViewData.ModelState.IsValid.Should().BeTrue();
            view.Model.Should().Be(model);
        }

        A.CallTo(() => Service.GetManyAsync(parameter)).MustHaveHappenedOnceExactly();
        A.CallTo(() => ModelFactory.ListModelAsync(paginatedList, parameter)).MustHaveHappenedOnceExactly();

    }
}
