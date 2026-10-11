namespace Example.Modules.Gauge;

public sealed class ColorSwatch
{
    public required Brush Brush { get; init; }

    public required ICommand Command { get; init; }

    public static ColorSwatch Solid(Color color, ICommand command) => new()
    {
        Brush = new SolidColorBrush(color),
        Command = command
    };

    public static ColorSwatch Gradient(Color start, Color end, ICommand command) => new()
    {
        Brush = new LinearGradientBrush([new GradientStop(start, 0), new GradientStop(end, 1)], new Point(0, 0), new Point(1, 0)),
        Command = command
    };
}
