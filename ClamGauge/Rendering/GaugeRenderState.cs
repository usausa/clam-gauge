namespace ClamGauge.Rendering;

internal sealed class GaugeRenderState
{
    public required double DisplayValue { get; init; }

    public required double MinValue { get; init; }

    public required double MaxValue { get; init; }

    public required float StartAngle { get; init; }

    public required float SweepAngle { get; init; }

    public required IList<GaugeRange> Ranges { get; init; }

    public required Color GaugeBackgroundColor { get; init; }

    public required Color GaugeBackgroundColor2 { get; init; }

    public required float BezelOuterThickness { get; init; }

    public required Color BezelOuterStartColor { get; init; }

    public required Color BezelOuterEndColor { get; init; }

    public required float BezelInnerThickness { get; init; }

    public required Color BezelInnerStartColor { get; init; }

    public required Color BezelInnerEndColor { get; init; }

    public required float ArcInnerExtent { get; init; }

    public required float ArcOuterExtent { get; init; }

    public required Color ArcTrackColor { get; init; }

    public required Color ArcStartColor { get; init; }

    public required Color ArcEndColor { get; init; }

    public required double MajorTickInterval { get; init; }

    public required float MajorTickInnerExtent { get; init; }

    public required float MajorTickOuterExtent { get; init; }

    public required float MajorTickThickness { get; init; }

    public required Color MajorTickColor { get; init; }

    public required double MinorTickInterval { get; init; }

    public required float MinorTickInnerExtent { get; init; }

    public required float MinorTickOuterExtent { get; init; }

    public required float MinorTickThickness { get; init; }

    public required Color MinorTickColor { get; init; }

    public required double LabelInterval { get; init; }

    public required float LabelExtent { get; init; }

    public required string LabelFormat { get; init; }

    public required IValueConverter? LabelConverter { get; init; }

    public required Func<double, string>? LabelFormatter { get; init; }

    public required float LabelFontSize { get; init; }

    public required Color LabelColor { get; init; }

    public required bool ShowValue { get; init; }

    public required string ValueFormat { get; init; }

    public required float ValueFontSize { get; init; }

    public required Color ValueColor { get; init; }

    public required string? Unit { get; init; }

    public required float UnitFontSize { get; init; }

    public required Color UnitColor { get; init; }

    public required GaugeUnitPosition UnitPosition { get; init; }

    public required float ValueOffsetY { get; init; }

    public required float NeedleEndExtent { get; init; }

    public required float NeedleStartWidth { get; init; }

    public required float NeedleEndWidth { get; init; }

    public required Color NeedleLeftColor { get; init; }

    public required Color NeedleRightColor { get; init; }

    public required float PivotRadius { get; init; }

    public required Color PivotColor { get; init; }
}
