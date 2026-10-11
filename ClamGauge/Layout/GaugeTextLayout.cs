namespace ClamGauge.Layout;

internal static class GaugeTextLayout
{
    public const float UnitBottomGap = 2f;

    public const float UnitRightGap = 4f;

    public static float CenterStart(float x, float width) => x - (width / 2f);

    public static float CenterBaseline(float y, float ascent, float descent) => y - ((ascent + descent) / 2f);

    public static (float Value, float Unit) StackBaselines(float y, float valueAscent, float valueDescent, float unitAscent, float unitDescent)
    {
        var valueHeight = valueDescent - valueAscent;
        var unitHeight = unitDescent - unitAscent;
        var top = y - ((valueHeight + UnitBottomGap + unitHeight) / 2f);
        return (top - valueAscent, top + valueHeight + UnitBottomGap - unitAscent);
    }

    public static (float Value, float Unit) RowStarts(float x, float valueWidth, float unitWidth)
    {
        var start = x - ((valueWidth + UnitRightGap + unitWidth) / 2f);
        return (start, start + valueWidth + UnitRightGap);
    }
}
