using FluentValidation;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class TestBase
{
    public TestBase()
    {
        Sut = new TestCrudController(Service, Router, ModelFactory, Validator);
    }

    protected Fixture Fixture { get; } = new();

    protected ICrudModelFactory<TestEntity, ListParameter> ModelFactory { get; } = Helpers.Fake<ICrudModelFactory<TestEntity, ListParameter>>();

    protected ICrudRouter<TestEntity, ListParameter> Router { get; } = Helpers.Fake<ICrudRouter<TestEntity, ListParameter>>();

    protected ICrudService<TestEntity> Service { get; } = Helpers.Fake<ICrudService<TestEntity>>();

    protected IValidator<TestEntity> Validator { get; } = Helpers.Fake<IValidator<TestEntity>>();

    protected AbstractCrudController<TestEntity, ListParameter, ICrudRouter<TestEntity, ListParameter>, ICrudService<TestEntity>> Sut { get; }
}
