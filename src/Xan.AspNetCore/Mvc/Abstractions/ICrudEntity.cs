using Xan.AspNetCore.Models;

namespace Xan.AspNetCore.Mvc.Abstractions;

public interface ICrudEntity
    : IEntity
    , IHasTimestamps
{
    ObjectState State { get; set; }
}
