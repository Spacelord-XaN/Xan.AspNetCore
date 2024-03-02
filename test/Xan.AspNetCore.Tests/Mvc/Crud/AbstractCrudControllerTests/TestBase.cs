using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class TestBase
{
    public TestBase()
    {
        Sut = new TestCrudController(Service, Router, ModelFactory, Validator)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    protected Fixture Fixture { get; } = new();

    protected ICrudModelFactory<TestEntity, ListParameter> ModelFactory { get; } = X.StrictFake<ICrudModelFactory<TestEntity, ListParameter>>();

    protected ICrudRouter Router { get; } = X.StrictFake<ICrudRouter>();

    protected ICrudService<TestEntity, ListParameter> Service { get; } = X.StrictFake<ICrudService<TestEntity, ListParameter>>();

    protected IValidator<TestEntity> Validator { get; } = X.StrictFake<IValidator<TestEntity>>();

    protected TestCrudController Sut { get; }
}
