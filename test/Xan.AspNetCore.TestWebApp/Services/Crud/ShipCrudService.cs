using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.AspNetCore.TestWebApp.Data;
using Xan.AspNetCore.TestWebApp.Models.Crud;

namespace Xan.AspNetCore.TestWebApp.Services.Crud;

public class ShipCrudService
    : AbstractCrudService<ShipEntity, ListParameter>
{
    private readonly TestWebAppDbContext _db;

    public ShipCrudService(TestWebAppDbContext db)
        : base(db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public override DbSet<ShipEntity> Set => _db.Ships;

    public override async Task<bool> CanDeleteAsync(ShipEntity entity)
        => await Task.FromResult(true);

    protected override Expression<Func<ShipEntity, bool>> Search(string searchString)
    {
        ArgumentNullException.ThrowIfNull(searchString);

        if (int.TryParse(searchString, out int id))
        {
            return b => b.Id == id;
        }
        return b => EF.Functions.Like(b.Name!, $"%{searchString}%");
    }

    protected override IQueryable<ShipEntity> OrderByDefault(IQueryable<ShipEntity> iq)
    {
        ArgumentNullException.ThrowIfNull(iq);

        return iq.OrderBy(b => b.Name);
    }
}
