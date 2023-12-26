using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class TestCrudService
    : AbstractCrudService<TestEntity, ListParameter>
{
    private readonly TestDbContext _db;

    public TestCrudService(TestDbContext db)
        : base(db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public override DbSet<TestEntity> Set => _db.Tests;

    public override Task<bool> CanDeleteAsync(TestEntity entity)
    {
        throw new NotImplementedException();
    }

    protected override Expression<Func<TestEntity, bool>> Search(string searchString)
    {
        throw new NotImplementedException();
    }

    protected override IQueryable<TestEntity> OrderByDefault(IQueryable<TestEntity> iq)
    {
        throw new NotImplementedException();
    }
}
