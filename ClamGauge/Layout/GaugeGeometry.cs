namespace ClamGauge.Layout;

internal sealed class GaugeGeometry
{
    public float CenterX { get; }

    public float CenterY { get; }

    public float OuterRadius { get; }

    public float Radius { get; }

    public float ArcRadius { get; }

    public float ArcThickness { get; }

    public GaugeGeometry(float width, float height, float bezelOuterThickness, float bezelInnerThickness, float arcInnerExtent, float arcOuterExtent)
    {
        CenterX = width / 2f;
        CenterY = height / 2f;
        OuterRadius = MathF.Min(CenterX, CenterY);
        Radius = OuterRadius - (bezelOuterThickness + bezelInnerThickness);
        ArcRadius = Radius * ((arcInnerExtent + arcOuterExtent) / 2f);
        ArcThickness = Radius * (arcOuterExtent - arcInnerExtent);
    }

    public (float X, float Y) GetPoint(float angle, float extent)
    {
        var radian = MathF.PI * GaugeScale.ToSkiaAngle(angle) / 180f;
        return (CenterX + (Radius * extent * MathF.Cos(radian)), CenterY + (Radius * extent * MathF.Sin(radian)));
    }
}
