using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TableBuilderExtensionsTests;

public class EditLinkColumn
    : TestBase
{
    private readonly ICrudRouter _router = X.StrictFake<ICrudRouter>();

    [Theory]
    [AutoData]
    public void CanDelete(TestEntity item, string url)
    {
        // Arrange
        A.CallTo(() => _router.ToEdit(item.Id, null)).Returns(url);
        TableBuilder<CrudItemModel<TestEntity>> sut = CreateSut([new CrudItemModel<TestEntity>(item, true)]);

        // Act
        TagBuilder result = sut.EditLinkColumn(_router).Build();

        // Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;"></th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;"><a href="{url}">Xan_AspNetCore_Edit</a></td></tr></tbody></table>""");
    }
}