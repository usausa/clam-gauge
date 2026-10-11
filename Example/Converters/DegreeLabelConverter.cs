namespace Example.Converters;

public sealed class DegreeLabelConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is double degree and < 360 ? String.Format(culture, "{0:N0}°", degree) : String.Empty;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
}
