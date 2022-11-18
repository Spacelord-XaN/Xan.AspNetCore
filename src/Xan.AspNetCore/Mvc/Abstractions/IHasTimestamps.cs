namespace Xan.AspNetCore.Mvc.Abstractions;

public interface IHasTimestamps
{
    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}
