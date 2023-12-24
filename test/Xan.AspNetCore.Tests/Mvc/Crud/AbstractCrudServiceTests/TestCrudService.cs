using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class TestCrudService
    : AbstractCrudService<TestEntity>
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
}
