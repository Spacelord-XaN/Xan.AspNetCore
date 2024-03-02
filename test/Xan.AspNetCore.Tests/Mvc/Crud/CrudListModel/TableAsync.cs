using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.Mvc.Crud.CrudListModel;

public class TableAsync
{
    [Theory]
    [AutoData]
    public async Task ShouldInitProperties(ListParameter parameter, CrudItemModel<TestEntity>[] items, int pageIndex, int pageSize, int totalPageCount, int totalItemCount, string listTitle, string createText, string htmlText)
    {
        //  Arrange
        IPaginatedList<CrudItemModel<TestEntity>> paginatedList = new PaginatedList<CrudItemModel<TestEntity>>(items, pageIndex, pageSize, totalPageCount, totalItemCount);
        ViewContext viewContext = new ();
        CrudListModel<TestEntity, ListParameter, ICrudRouter>.CreateTableDelegate createTableDelegate = X.StrictFake<CrudListModel<TestEntity, ListParameter, ICrudRouter>.CreateTableDelegate>();
        IHtmlContent htmlContent = new HtmlString(htmlText);
        A.CallTo(() => createTableDelegate.Invoke(viewContext, paginatedList)).Returns(htmlContent);
        ICrudRouter router = X.StrictFake<ICrudRouter>();
        A.CallTo(() => router.Equals(router)).Returns(true);
        CrudListModel<TestEntity, ListParameter, ICrudRouter> sut = new(paginatedList, parameter, createTableDelegate, router, listTitle, createText);

        //  Act
        IHtmlContent result = await sut.TableAsync(viewContext);

        //  Assert
        result.Should().BeHtml(htmlText);
        
        A.CallTo(() => createTableDelegate.Invoke(viewContext, paginatedList)).MustHaveHappenedOnceExactly();
    }
}
