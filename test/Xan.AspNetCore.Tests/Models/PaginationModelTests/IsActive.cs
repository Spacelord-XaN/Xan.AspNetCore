using Xan.AspNetCore.Models;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Tests.Models.PaginationModelTests;

public class IsActive
{
    [Theory]
    [AutoData]
    public void ShoulReturnTrue(int pageIndex)
    {
        //  Arrange
        PaginationModel sut = new(new PaginatedList<object>([], pageIndex, 0, 0, 0), new ListParameter(), parameter => string.Empty);

        //  Act
        bool result = sut.IsActive(pageIndex);

        //  Assert
        result.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void ShoulReturnFalse(int pageIndex, int page)
    {
        //  Arrange
        PaginationModel sut = new(new PaginatedList<object>([], pageIndex, 0, 0, 0), new ListParameter(), parameter => string.Empty);

        //  Act
        bool result = sut.IsActive(page);

        //  Assert
        result.Should().BeFalse();
    }
}
