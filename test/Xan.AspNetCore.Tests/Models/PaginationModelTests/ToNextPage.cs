using Xan.AspNetCore.Models;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.Models.PaginationModelTests;

public class ToNextPage
{
    [Theory]
    [AutoData]
    public void ShouldCallDelegate(string url, ListParameter listParameter)
    {
        //  Arrange
        Func<ListParameter, string> getListUrl = X.StrictFake<Func<ListParameter, string>>();
        A.CallTo(() => getListUrl.Invoke(A<ListParameter>.Ignored)).Returns(url);
        PaginationModel sut = new(new PaginatedList<object>([], 0, 0, 0, 0), listParameter, getListUrl);

        //  Act
        string result = sut.ToNextPage();

        //  Assert
        result.Should().Be(url);
        A.CallTo(() => getListUrl.Invoke(A<ListParameter>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
