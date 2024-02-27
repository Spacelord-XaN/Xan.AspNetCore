using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.ChangeTracking.EntityEntryExtensionsTests;

public class TestBase
    : IDisposable
{
    private readonly SqliteConnection _connection;

    public TestBase()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        using (TestDbContext context = new(options))
        {
            context.Database.EnsureCreated();
        }

        Db = new(options);
    }

    public virtual void Dispose()
    {
        Db.Dispose();
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }

    public TestDbContext Db { get; }
}

public class TestDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<TestEntity> TestEntities => Set<TestEntity>();
}

public class TestEntity
    : IEntity
{
    public int Id { get;set; }
}
