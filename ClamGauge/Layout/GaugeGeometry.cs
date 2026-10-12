namespace ClamGauge.Layout;

using SkiaExtension;

using SkiaSharp;

internal readonly record struct GaugeGeometry
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

    public SKPoint GetPoint(float angle, float extent) => AngleHelper.GetPoint(new SKPoint(CenterX, CenterY), Radius * extent, AngleHelper.ToSkiaAngle(angle));
}
