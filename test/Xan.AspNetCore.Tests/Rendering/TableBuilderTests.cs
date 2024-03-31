using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering;

public class TableBuilderTests
{
    public class MyEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }

    [Theory]
    [AutoData]
    public void ShouldReturnCorrectHtml(MyEntity item1, MyEntity item2)
    {
        //  Arrange
        StringLocalizerMock localizer = new();
        TableBuilder<MyEntity> sut = new([item1, item2], new DefaultHtmlFactory(localizer), localizer);
        sut.Column(c => c.Title("Name").For(item => item.Name));
        sut.Column(0, c => c.Title("Id").For(item => item.Id));
        sut.Column(c => c.Title("Description").For(item => item.Description));

        //  Act
        TagBuilder result = sut.Build();

        //  Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;">Id</th><th scope="col" style="width: auto;text-align: left;white-space: nowrap;">Name</th><th scope="col" style="width: auto;text-align: left;white-space: nowrap;">Description</th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;">{item1.Id}</td><td style="width: auto;text-align: left;white-space: nowrap;">{item1.Name}</td><td style="width: auto;text-align: left;white-space: nowrap;">{item1.Description}</td></tr><tr><td style="width: auto;text-align: left;white-space: nowrap;">{item2.Id}</td><td style="width: auto;text-align: left;white-space: nowrap;">{item2.Name}</td><td style="width: auto;text-align: left;white-space: nowrap;">{item2.Description}</td></tr></tbody></table>""");
    }
}
