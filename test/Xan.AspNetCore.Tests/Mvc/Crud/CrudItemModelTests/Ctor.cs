using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.CrudItemModelTests;

public class Ctor
{
    [Theory]
    [AutoData]
    public void ShouldInitProperties(TestEntity entity, bool canDelete)
    {
        //  Arrange

        //  Act
        CrudItemModel<TestEntity> sut = new(entity, canDelete);

        //  Assert
        using (new AssertionScope())
        {
            sut.Entity.Should().Be(entity);
            sut.CanDelete.Should().Be(canDelete);
        }
    }
}
