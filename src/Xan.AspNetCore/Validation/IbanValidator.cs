using FluentValidation;
using FluentValidation.Validators;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Xan.AspNetCore.Validation;

public sealed class IbanValidator<T>
    : PropertyValidator<T, string?>
{
    private const string _alphabet = "abcdefghijklmnopqrstuvwxyz";

    private static bool IsValid(string input)
    {
        //  https://de.wikipedia.org/wiki/Internationale_Bankkontonummer
        string trimmed = input.Replace(" ", "", StringComparison.InvariantCultureIgnoreCase);
        if (trimmed.Length < 15 || trimmed.Length > 30)
        {
            return false;
        }
        string normalized = trimmed.Substring(4, trimmed.Length - 4) + trimmed.Substring(0, 4);
        StringBuilder numberString = new();
        foreach (char c in normalized)
        {
            if (char.IsDigit(c))
            {
                numberString.Append(c);
            }
            else if (_alphabet.Contains(c, StringComparison.InvariantCultureIgnoreCase))
            {
                numberString.Append($"{_alphabet.IndexOf(c, StringComparison.InvariantCultureIgnoreCase) + 10}");
            }
        }
        BigInteger number = BigInteger.Parse(numberString.ToString(), CultureInfo.InvariantCulture);
        return number % 97 == 1;
    }

    public override bool IsValid(ValidationContext<T> context, string? iban)
    {
        if (!string.IsNullOrEmpty(iban))
        {
            return IsValid(iban);
        }
        return true;
    }

    public override string Name => "IbanValidator";
}

public static class IbanValidatorExtensions
{
    public static IRuleBuilderOptions<T, string?> Iban<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new IbanValidator<T>());
    }
}
