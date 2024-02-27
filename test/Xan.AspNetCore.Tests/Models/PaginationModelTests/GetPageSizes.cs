using Xan.AspNetCore.Models;
using Xan.Extensions.Collections;

namespace Xan.AspNetCore.Tests.Models.PaginationModelTests;

public class GetPageSizes
{
    [Fact]
    public void ShouldReturnThePageSizes()
    {
        // Arrange

        // Act
        var result = PaginationModel.GetPageSizes();

        // Assert
        result.Should().BeEquivalentTo([5, 10, IPaginatedList.AllPageSize]);
    }
}
