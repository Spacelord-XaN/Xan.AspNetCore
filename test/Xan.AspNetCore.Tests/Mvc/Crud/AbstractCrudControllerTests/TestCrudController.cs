using FluentValidation;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class TestCrudController
    : AbstractCrudController<TestEntity, ListParameter, ICrudRouter<TestEntity, ListParameter>, ICrudService<TestEntity>>
{
    public TestCrudController(ICrudService<TestEntity> service, ICrudRouter<TestEntity, ListParameter> router, ICrudModelFactory<TestEntity, ListParameter> modelFactory, IValidator<TestEntity> validator)
        : base(service, router, modelFactory, validator)
    { }

    protected override IQueryable<TestEntity> GetMany(ListParameter parameter)
    {
        throw new NotImplementedException();
    }
}
