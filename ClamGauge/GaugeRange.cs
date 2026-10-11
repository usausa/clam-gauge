namespace ClamGauge;

public sealed class GaugeRange
{
    public double StartValue { get; set; }

    public double EndValue { get; set; }

    public Color StartColor { get; set; } = Colors.Transparent;

    public Color? EndColor { get; set; }
}
