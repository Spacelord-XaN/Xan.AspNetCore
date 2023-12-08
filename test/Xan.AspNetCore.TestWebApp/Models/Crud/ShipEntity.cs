using FluentValidation;

namespace Xan.AspNetCore.TestWebApp.Models.Crud;

public sealed class ShipEntity
    : AbstractCrudEntity
{
    public string? Name { get; set; }

    public int LengthInMeters { get; set; }
}

public sealed class ShipEntityValidator
    : AbstractValidator<ShipEntity>
{
    public ShipEntityValidator()
    {
        RuleFor(seller => seller.LengthInMeters)
            .NotEqual(0)
            .WithMessage("Please enter a length other than 0");
    }
}
