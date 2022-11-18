using Microsoft.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public sealed class TestDbContext
    : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    { }

    public DbSet<TestEntity> Tests => Set<TestEntity>();
}
