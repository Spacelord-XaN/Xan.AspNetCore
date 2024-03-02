using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.CrudRouterExtensionsTests;

public class ToDeleteOrToggleState
{
    [Theory]
    [AutoData]
    public void CanDelete_ShouldReturnDeleteUrl(TestEntity entity, string url)
    {
        //  Arrange
        ICrudRouter router = X.StrictFake<ICrudRouter>();
        A.CallTo(() => router.ToDelete(A<int>._)).Returns(url);
        CrudItemModel<TestEntity> model = new(entity, true);

        //  Act
        string result = router.ToDeleteOrToggleState(model);

        //  Assert
        result.Should().Be(url);

        A.CallTo(() => router.ToDelete(entity.Id)).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public void CannotDelete_Enabled_ShouldReturnDisableUrl(TestEntity entity, string url)
    {
        //  Arrange
        ICrudRouter router = X.StrictFake<ICrudRouter>();
        A.CallTo(() => router.ToDisable(A<int>._)).Returns(url);

        entity.State = ObjectState.Enabled;
        CrudItemModel<TestEntity> model = new(entity, false);

        //  Act
        string result = router.ToDeleteOrToggleState(model);

        //  Assert
        result.Should().Be(url);

        A.CallTo(() => router.ToDisable(entity.Id)).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public void CannotDelete_Disabled_ShouldReturnEnableUrl(TestEntity entity, string url)
    {
        //  Arrange
        ICrudRouter router = X.StrictFake<ICrudRouter>();
        A.CallTo(() => router.ToEnable(A<int>._)).Returns(url);

        entity.State = ObjectState.Disabled;
        CrudItemModel<TestEntity> model = new(entity, false);

        //  Act
        string result = router.ToDeleteOrToggleState(model);

        //  Assert
        result.Should().Be(url);

        A.CallTo(() => router.ToEnable(entity.Id)).MustHaveHappenedOnceExactly();
    }
}
