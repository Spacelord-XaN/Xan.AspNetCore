using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Tests.Mvc.Crud;

public class TestEntity
    : ICrudEntity
{
    public ObjectState State { get; set; }
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
