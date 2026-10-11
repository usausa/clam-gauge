namespace ClamGauge.Layout;

internal readonly record struct GaugeScale(double MinValue, double MaxValue, float StartAngle, float SweepAngle)
{
    public static float ToSkiaAngle(float angle) => angle - 90f;

    public float ToAngle(double value)
    {
        var fraction = (value - MinValue) / (MaxValue - MinValue);
        return StartAngle + (float)(fraction * SweepAngle);
    }

    public List<double> GetMarks(double interval)
    {
        var marks = new List<double>();
        if (interval <= 0)
        {
            return marks;
        }

        var count = (int)Math.Round((MaxValue - MinValue) / interval);
        for (var i = 0; i <= count; i++)
        {
            var value = MinValue + (i * interval);
            if (value > MaxValue + (interval * 0.001))
            {
                break;
            }

            marks.Add(Math.Min(value, MaxValue));
        }

        return marks;
    }

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
