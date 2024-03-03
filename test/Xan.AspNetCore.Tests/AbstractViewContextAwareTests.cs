using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests;

public class AbstractViewContextAwareTests
{
    public class MySut
        : AbstractViewContextAware
    {
    }

    [Fact]
    public void NotCalled_ShouldThrow()
    {
        //  Arrange
        MySut sut = new();

        //  Act
        Action act = () => _ = sut.ViewContext;

        //  Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Called_ShouldReturnInstance()
    {
        //  Arrange
        MySut sut = new();
        ViewContext viewContext = new();
        sut.Contextualize(viewContext);

        //  Act
        ViewContext result = sut.ViewContext;

        //  Assert
        result.Should().BeSameAs(viewContext);
    }
}
