using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.AbstractSelectListServiceTests;

public class States
{
    private readonly StringLocalizerMock _stringLocalizer = new ();
    private readonly List<SelectListItem> _expectedItems = [
        new SelectListItem { Value = "Disabled", Text = "Xan_AspNetCore_Disabled" },
        new SelectListItem { Value = "Enabled", Text = "Xan_AspNetCore_Enabled" },
    ];

    [Fact]
    public void DoNotIncludeAll_ShouldContainAllEnumValues()
    {
        //  Arrange
        AbstractSelectListService sut = new (_stringLocalizer);

        //  Act
        SelectList result = sut.States();

        //  Assert
        result.Should().BeEquivalentTo(_expectedItems);
    }

    [Fact]
    public void IncludeAll_ShouldContainAllEnumValuesAndAll()
    {
        //  Arrange
        AbstractSelectListService sut = new(_stringLocalizer);
        _expectedItems.Insert(0, new SelectListItem { Value = "", Text = "Xan_AspNetCore_AllStates" });

        //  Act
        SelectList result = sut.States(true);

        //  Assert
        result.Should().BeEquivalentTo(_expectedItems);
    }
}
