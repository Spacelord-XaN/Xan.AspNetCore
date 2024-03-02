namespace Xan.AspNetCore.Rendering;

public sealed class Width
{
    private enum Unit
    {
        Percent,
        Pixel
    }

    public static Width Auto { get; } = new();

    public static Width Zero { get; } = new(0, Unit.Pixel);

    public static Width Percent(int percentage)
        => new(percentage, Unit.Percent);

    private readonly int? _percentage;
    private readonly bool _isAuto;
    private readonly Unit _unit;

    private Width(int percentage, Unit unit)
    {
        _percentage = percentage;
        _isAuto = false;
        _unit = unit;
    }

    private Width()
    {
        _isAuto = true;
    }

    public string GetStyle()
    {
        string format = "width: {0};";
        if (_isAuto)
        {
            return string.Format(format, "auto");
        }

        if (_percentage.HasValue)
        {
            return string.Format(format, $"{_percentage.Value}{GetUnit()}");
        }
        return "";
    }

    private string GetUnit()
    {
        if (_unit == Unit.Pixel)
        {
            return "px";
        }
        return "%";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Width other)
        {
            return _isAuto == other._isAuto
                && _percentage == other._percentage
                && _unit == other._unit;
        }
        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(_percentage, _isAuto, _unit);

    public override string ToString()
    {
        if (_isAuto)
        {
            return "Auto";
        }
        if (_percentage.HasValue)
        {
            return $"{_percentage.Value}{GetUnit()}";
        }
        return GetUnit();
    }
}
