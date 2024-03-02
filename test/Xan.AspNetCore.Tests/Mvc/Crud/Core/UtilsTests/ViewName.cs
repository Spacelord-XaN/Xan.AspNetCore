using Xan.AspNetCore.Mvc.Crud.Core;

namespace Xan.AspNetCore.Tests.Mvc.Crud.Core.UtilsTests;

public class ViewName
{
    [Theory]
    [InlineData("", "Crud")]
    [InlineData("Details", "CrudDetails")]
    public void ShouldPrefixWithCrud(string input, string expectedResult)
    {
        //  Arrange

        //  Act
        string result = Utils.ViewName(input);

        //  Assert
        result.Should().Be(expectedResult);
    }
}
