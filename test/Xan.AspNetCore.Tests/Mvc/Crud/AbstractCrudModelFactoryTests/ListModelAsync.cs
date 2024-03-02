using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudModelFactoryTests;

public class ListModelAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldReturnNewModel(ListParameter parameter, CrudItemModel<TestEntity>[] items, int pageIndex, int pageSize, int totalPageCount, int totalItemCount)
    {
        //  Arrange
        A.CallTo(() => Router.Equals(Router)).Returns(true);
        IPaginatedList<CrudItemModel<TestEntity>> paginatedList = new PaginatedList<CrudItemModel<TestEntity>>(items, pageIndex, pageSize, totalPageCount, totalItemCount);

        //  Act
        ICrudListModel result = await Sut.ListModelAsync(paginatedList, parameter);

        //  Assert
        using (new AssertionScope())
        {
            CrudListModel<TestEntity, ListParameter, ICrudRouter> listModel = result.Should().BeOfType<CrudListModel<TestEntity, ListParameter, ICrudRouter>>().Subject;

            listModel.CreateText.Should().Be(TestCrudModelFactory.CreateTitleString);
            listModel.ListTitle.Should().Be(TestCrudModelFactory.ListTitleString);
            listModel.PageIndex.Should().Be(pageIndex);
            listModel.PageSize.Should().Be(pageSize);
            listModel.Parameter.Should().Be(parameter);
            listModel.Router.Should().Be(Router);
        }
    }
}
