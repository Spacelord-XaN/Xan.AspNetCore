using FluentValidation;
using Moq;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class AbstractCrudControllerTest
    : IDisposable
{
    public AbstractCrudControllerTest()
    {
        Fixture = new Fixture()
            .Customize(new AutoMoqCustomization());

        Sut = new TestCrudController(MockService.Object, MockRouter.Object, MockModelFactory.Object, MockValidator.Object);
    }

    protected IFixture Fixture { get; }

    protected Mock<ICrudModelFactory<TestEntity, ListParameter>> MockModelFactory { get; } = new Mock<ICrudModelFactory<TestEntity, ListParameter>>();

    protected Mock<ICrudRouter<TestEntity>> MockRouter { get; } = new Mock<ICrudRouter<TestEntity>>();

    protected Mock<ICrudService<TestEntity>> MockService { get; } = new Mock<ICrudService<TestEntity>>();

    protected Mock<IValidator<TestEntity>> MockValidator { get; } = new Mock<IValidator<TestEntity>>();

    protected AbstractCrudController<TestEntity, ListParameter> Sut { get; }

    public void Dispose()
    {
        MockModelFactory.VerifyNoOtherCalls();
        MockRouter.VerifyNoOtherCalls();
        MockService.VerifyNoOtherCalls();
        MockValidator.VerifyNoOtherCalls();

        GC.SuppressFinalize(this);
    }
}
