using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public sealed class TestDbContextFactory
    : IDisposable
{
    private readonly SqliteConnection _connection = new("DataSource=:memory:");
    private readonly DbContextOptionsBuilder<TestDbContext> _builder = new();

    public TestDbContextFactory()
    {
        _connection.Open();
        _builder.UseSqlite(_connection);
        Context = new(_builder.Options);
    }

    public TestDbContext Context { get; }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}
