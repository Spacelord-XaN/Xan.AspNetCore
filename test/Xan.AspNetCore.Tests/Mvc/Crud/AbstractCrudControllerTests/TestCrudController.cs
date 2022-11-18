using FluentValidation;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class TestCrudController
    : AbstractCrudController<TestEntity, ListParameter>
{
    public TestCrudController(ICrudService<TestEntity> service, ICrudRouter<TestEntity> router, ICrudModelFactory<TestEntity, ListParameter> modelFactory, IValidator<TestEntity> validator)
        : base(service, router, modelFactory, validator)
    { }
}
