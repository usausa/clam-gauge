namespace ClamGauge.Layout;

internal readonly record struct GaugeScale(double MinValue, double MaxValue, float StartAngle, float SweepAngle)
{
    public float ToAngle(double value)
    {
        var fraction = (value - MinValue) / (MaxValue - MinValue);
        return StartAngle + (float)(fraction * SweepAngle);
    }

    public int GetMarkCount(double interval)
    {
        if (interval <= 0)
        {
            return 0;
        }

        var count = (int)Math.Round((MaxValue - MinValue) / interval);
        return MinValue + (count * interval) > MaxValue + (interval * 0.001) ? count : count + 1;
    }

    public double GetMark(double interval, int index) => Math.Min(MinValue + (index * interval), MaxValue);

    public bool TryGetArc(double startValue, double endValue, out float startAngle, out float sweepAngle)
    {
        var start = Math.Clamp(startValue, MinValue, MaxValue);
        var end = Math.Clamp(endValue, MinValue, MaxValue);
        if (start >= end)
        {
            startAngle = 0;
            sweepAngle = 0;
            return false;
        }

        startAngle = ToAngle(start);
        sweepAngle = ToAngle(end) - startAngle;
        return sweepAngle > 0;
    }
}
