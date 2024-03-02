using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Mvc.Crud.Core;

namespace Xan.AspNetCore.Tests.Mvc.Crud.Core.UtilsTests;

public class ControllerName
{
    private class AbstractCrudEntity
        : ICrudEntity
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObjectState State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime UpdatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    private class MyEntity
        : AbstractCrudEntity
    {
    }

    [Fact]
    public void MyEntity_ShouldRemoveEntityFromName()
    {
        //  Arrange

        //  Act
        string result = Utils.ControllerNameForEntity<MyEntity>();

        //  Assert
        result.Should().Be("My");
    }

    private class OtherEntityController
        : AbstractCrudEntity
    {
    }

    [Fact]
    public void OtherEntityController_ShouldRemoveEntityFromName()
    {
        //  Arrange

        //  Act
        string result = Utils.ControllerNameForEntity<OtherEntityController>();

        //  Assert
        result.Should().Be("OtherController");
    }

    private class Ship
        : AbstractCrudEntity
    {
    }

    [Fact]
    public void Ship_ShouldRemoveEntityFromName()
    {
        //  Arrange

        //  Act
        string result = Utils.ControllerNameForEntity<Ship>();

        //  Assert
        result.Should().Be("Ship");
    }
}
