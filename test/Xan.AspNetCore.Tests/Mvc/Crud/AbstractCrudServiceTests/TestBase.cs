using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

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
        Sut = new(Db);
    }

    public virtual void Dispose()
    {
        Db.Dispose();
        _connection.Dispose();
        GC.SuppressFinalize(this);
    }

    public TestDbContext Db { get; }

    public TestCrudService Sut { get; }
}
