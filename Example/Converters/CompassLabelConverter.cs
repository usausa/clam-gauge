namespace Example.Converters;

public sealed class CompassLabelConverter : IValueConverter
{
    private static readonly string[] Names = ["N", "NE", "E", "SE", "S", "SW", "W", "NW"];

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var degree = value is double angle ? (int)Math.Round(angle) : -1;
        return (degree >= 0) && (degree % 45 == 0) ? Names[(degree / 45) % Names.Length] : String.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}
