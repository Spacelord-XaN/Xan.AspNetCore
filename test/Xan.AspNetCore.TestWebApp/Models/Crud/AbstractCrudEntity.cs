using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.TestWebApp.Models.Crud;

public abstract class AbstractCrudEntity
    : ICrudEntity
{
    public int Id { get; set; }

    public ObjectState State { get; set; } = ObjectState.Enabled;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
