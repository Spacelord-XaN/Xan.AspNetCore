using Microsoft.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public sealed class TestDbContext(DbContextOptions<TestDbContext> options)
    : DbContext(options)
{
    public DbSet<TestEntity> Tests => Set<TestEntity>();
}
