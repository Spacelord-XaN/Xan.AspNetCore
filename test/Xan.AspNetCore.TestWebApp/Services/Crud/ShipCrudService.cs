using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.TestWebApp.Data;
using Xan.AspNetCore.TestWebApp.Models.Crud;

namespace Xan.AspNetCore.TestWebApp.Services.Crud;

public class ShipCrudService
    : AbstractCrudService<ShipEntity>
{
    private readonly TestWebAppDbContext _db;

    public ShipCrudService(TestWebAppDbContext db)
        : base(db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public override DbSet<ShipEntity> Set => _db.Ships;

    public override async Task<bool> CanDeleteAsync(ShipEntity entity)
    {
        return await Task.FromResult(true);
    }

    public override IQueryable<ShipEntity> DefaultOrder(IQueryable<ShipEntity> set)
    {
        return set.OrderBy(s => s.Name);
    }

    public override Expression<Func<ShipEntity, bool>> Search(string searchString)
    {
        ArgumentNullException.ThrowIfNull(searchString);

        if (int.TryParse(searchString, out int id))
        {
            return b => b.Id == id;
        }
        return b => EF.Functions.Like(b.Name!, $"%{searchString}%");
    }
}
