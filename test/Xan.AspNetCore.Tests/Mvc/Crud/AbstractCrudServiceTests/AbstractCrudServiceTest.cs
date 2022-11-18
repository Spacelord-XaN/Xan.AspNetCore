namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public abstract class AbstractCrudServiceTest
    : IDisposable
{
    private readonly TestDbContextFactory _testDbContextFactory = new();

    protected TestDbContext Context { get => _testDbContextFactory.Context; }

    public virtual void Dispose()
    {
        _testDbContextFactory.Dispose();

        GC.SuppressFinalize(this);
    }
}
