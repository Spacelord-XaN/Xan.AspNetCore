using Xan.AspNetCore.Validation;

namespace Xan.AspNetCore.Tests.Validation;

public class IbanValidatorTests
{
    [Theory]
    [InlineData("DE68 2105 0170 0012 3456 78")]
    [InlineData("DE68210501700012345678")]
    public void IsValid(string iban)
    {
        Assert.True(IbanValidator<object>.IsValid(iban));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("ATpp bbbb bkkk kkkk kkkk ")]
    public void IsNotValid(string? iban)
    {
        Assert.False(IbanValidator<object>.IsValid(iban));
    }
}
