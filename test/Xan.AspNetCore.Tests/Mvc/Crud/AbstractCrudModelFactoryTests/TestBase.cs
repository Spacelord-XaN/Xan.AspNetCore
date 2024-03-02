using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudModelFactoryTests;

public class TestBase
{
    public TestBase()
    {
        Sut = new(Router);
    }

    public ICrudRouter Router { get; } = X.StrictFake<ICrudRouter>();

    public TestCrudModelFactory Sut { get; }
}
