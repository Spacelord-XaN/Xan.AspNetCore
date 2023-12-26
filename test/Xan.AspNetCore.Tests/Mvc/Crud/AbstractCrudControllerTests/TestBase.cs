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

    protected ICrudRouter Router { get; } = Helpers.Fake<ICrudRouter>();

    protected ICrudService<TestEntity, ListParameter> Service { get; } = Helpers.Fake<ICrudService<TestEntity, ListParameter>>();

    protected IValidator<TestEntity> Validator { get; } = Helpers.Fake<IValidator<TestEntity>>();

    protected TestCrudController Sut { get; }
}
