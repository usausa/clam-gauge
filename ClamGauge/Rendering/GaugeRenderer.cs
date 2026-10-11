namespace ClamGauge.Rendering;

using System.Globalization;

using ClamGauge.Layout;

using SkiaSharp;
using SkiaSharp.Views.Maui;

internal static class GaugeRenderer
{
    private const float BackgroundOverlap = 1.5f;

    public static string FormatLabel(GaugeRenderState state, double value)
    {
        ArgumentNullException.ThrowIfNull(state);
        if (state.LabelFormatter is { } formatter)
        {
            return formatter(value);
        }

        if (state.LabelConverter is { } converter)
        {
            return converter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture) as string ?? String.Empty;
        }

        return String.Format(CultureInfo.CurrentCulture, state.LabelFormat, value);
    }

    public static string FormatValue(GaugeRenderState state)
    {
        ArgumentNullException.ThrowIfNull(state);
        return String.Format(CultureInfo.CurrentCulture, state.ValueFormat, state.DisplayValue);
    }

    public static SKColor GetPivotHighlight(SKColor color) =>
        new((byte)Math.Min(color.Red + 80, 255), (byte)Math.Min(color.Green + 50, 255), (byte)Math.Min(color.Blue + 40, 255));

    public static void Render(SKCanvas canvas, float width, float height, GaugeRenderState state)
    {
        ArgumentNullException.ThrowIfNull(canvas);
        ArgumentNullException.ThrowIfNull(state);
        canvas.Clear();
        if ((width <= 0) || (height <= 0) || (state.MaxValue <= state.MinValue))
        {
            return;
        }

        var geometry = new GaugeGeometry(width, height, state.BezelOuterThickness, state.BezelInnerThickness, state.ArcInnerExtent, state.ArcOuterExtent);
        if (geometry.Radius <= 0)
        {
            return;
        }

        var scale = new GaugeScale(state.MinValue, state.MaxValue, state.StartAngle, state.SweepAngle);
        DrawBackground(canvas, geometry, state);
        DrawBezel(canvas, geometry, state);
        DrawArcTrack(canvas, geometry, state);
        if (state.Ranges.Count == 0)
        {
            DrawArcFill(canvas, geometry, state);
        }
        else
        {
            DrawRanges(canvas, geometry, scale, state);
        }

        DrawTicks(canvas, geometry, scale, state.MinorTickInterval, state.MinorTickInnerExtent, state.MinorTickOuterExtent, state.MinorTickThickness, state.MinorTickColor);
        DrawTicks(canvas, geometry, scale, state.MajorTickInterval, state.MajorTickInnerExtent, state.MajorTickOuterExtent, state.MajorTickThickness, state.MajorTickColor);
        DrawLabels(canvas, geometry, scale, state);
        DrawCenterValue(canvas, geometry, state);
        DrawNeedle(canvas, geometry, scale, state);
        DrawPivot(canvas, geometry, state);
    }

    //--------------------------------------------------------------------------------
    // Face
    //--------------------------------------------------------------------------------

    private static void DrawBackground(SKCanvas canvas, GaugeGeometry geometry, GaugeRenderState state)
    {
        var gradient = state.GaugeBackgroundColor2.Alpha > 0f;
        if (!gradient && (state.GaugeBackgroundColor.Alpha <= 0f))
        {
            return;
        }

        var radius = geometry.Radius + BackgroundOverlap;
        var cx = geometry.CenterX;
        var cy = geometry.CenterY;
        using var shader = gradient
            ? SKShader.CreateLinearGradient(new SKPoint(cx - radius, cy - radius), new SKPoint(cx + radius, cy + radius), [state.GaugeBackgroundColor.ToSKColor(), state.GaugeBackgroundColor2.ToSKColor()], null, SKShaderTileMode.Clamp)
            : null;
        using var paint = new SKPaint();
        paint.Style = SKPaintStyle.Fill;
        paint.IsAntialias = true;
        if (shader is null)
        {
            paint.Color = state.GaugeBackgroundColor.ToSKColor();
        }
        else
        {
            paint.Shader = shader;
        }

        canvas.DrawCircle(cx, cy, radius, paint);
    }

    private static void DrawBezel(SKCanvas canvas, GaugeGeometry geometry, GaugeRenderState state)
    {
        var cx = geometry.CenterX;
        var cy = geometry.CenterY;
        var outer = geometry.OuterRadius;
        if (state.BezelOuterThickness > 0)
        {
            var radius = outer - (state.BezelOuterThickness / 2f);
            DrawRing(canvas, cx, cy, radius, state.BezelOuterThickness, outer, state.BezelOuterStartColor, state.BezelOuterEndColor);
        }

        if (state.BezelInnerThickness > 0)
        {
            var radius = outer - state.BezelOuterThickness - (state.BezelInnerThickness / 2f);
            DrawRing(canvas, cx, cy, radius, state.BezelInnerThickness, radius, state.BezelInnerStartColor, state.BezelInnerEndColor);
        }
    }

    private static void DrawRing(SKCanvas canvas, float cx, float cy, float radius, float thickness, float gradientRadius, Color startColor, Color endColor)
    {
        using var shader = SKShader.CreateLinearGradient(new SKPoint(cx - gradientRadius, cy - gradientRadius), new SKPoint(cx + gradientRadius, cy + gradientRadius), [startColor.ToSKColor(), endColor.ToSKColor()], null, SKShaderTileMode.Clamp);
        using var paint = new SKPaint();
        paint.Style = SKPaintStyle.Stroke;
        paint.StrokeWidth = thickness;
        paint.IsAntialias = true;
        paint.Shader = shader;
        canvas.DrawCircle(cx, cy, radius, paint);
    }

    //--------------------------------------------------------------------------------
    // Arc
    //--------------------------------------------------------------------------------

    private static void DrawArcTrack(SKCanvas canvas, GaugeGeometry geometry, GaugeRenderState state)
    {
        if (state.ArcTrackColor.Alpha <= 0f)
        {
            return;
        }

        using var paint = CreateArcPaint(geometry);
        paint.Color = state.ArcTrackColor.ToSKColor();
        DrawArc(canvas, geometry, state.StartAngle, state.SweepAngle, paint);
    }

    private static void DrawArcFill(SKCanvas canvas, GaugeGeometry geometry, GaugeRenderState state)
    {
        if (state.SweepAngle <= 0)
        {
            return;
        }

        DrawGradientArc(canvas, geometry, state.StartAngle, state.SweepAngle, state.ArcStartColor, state.ArcEndColor);
    }

    private static void DrawRanges(SKCanvas canvas, GaugeGeometry geometry, GaugeScale scale, GaugeRenderState state)
    {
        foreach (var range in state.Ranges)
        {
            if (scale.TryGetArc(range.StartValue, range.EndValue, out var startAngle, out var sweepAngle))
            {
                DrawGradientArc(canvas, geometry, startAngle, sweepAngle, range.StartColor, range.EndColor ?? range.StartColor);
            }
        }
    }

    private static void DrawGradientArc(SKCanvas canvas, GaugeGeometry geometry, float startAngle, float sweepAngle, Color startColor, Color endColor)
    {
        var center = new SKPoint(geometry.CenterX, geometry.CenterY);
        using var sweep = SKShader.CreateSweepGradient(center, [startColor.ToSKColor(), endColor.ToSKColor()], [0f, 1f], SKShaderTileMode.Clamp, 0f, sweepAngle);
        using var shader = sweep.WithLocalMatrix(SKMatrix.CreateRotationDegrees(GaugeScale.ToSkiaAngle(startAngle), center.X, center.Y));
        using var paint = CreateArcPaint(geometry);
        paint.Shader = shader;
        DrawArc(canvas, geometry, startAngle, sweepAngle, paint);
    }

    private static SKPaint CreateArcPaint(GaugeGeometry geometry) => new()
    {
        Style = SKPaintStyle.Stroke,
        StrokeWidth = geometry.ArcThickness,
        IsAntialias = true,
        StrokeCap = SKStrokeCap.Butt
    };

    private static void DrawArc(SKCanvas canvas, GaugeGeometry geometry, float startAngle, float sweepAngle, SKPaint paint)
    {
        var radius = geometry.ArcRadius;
        using var builder = new SKPathBuilder();
        builder.AddArc(new SKRect(geometry.CenterX - radius, geometry.CenterY - radius, geometry.CenterX + radius, geometry.CenterY + radius), GaugeScale.ToSkiaAngle(startAngle), sweepAngle);
        using var path = builder.Detach();
        canvas.DrawPath(path, paint);
    }

    //--------------------------------------------------------------------------------
    // Scale
    //--------------------------------------------------------------------------------

    private static void DrawTicks(SKCanvas canvas, GaugeGeometry geometry, GaugeScale scale, double interval, float innerExtent, float outerExtent, float thickness, Color color)
    {
        if (interval <= 0)
        {
            return;
        }

        using var paint = new SKPaint();
        paint.Style = SKPaintStyle.Stroke;
        paint.StrokeWidth = thickness;
        paint.IsAntialias = true;
        paint.Color = color.ToSKColor();
        paint.StrokeCap = SKStrokeCap.Butt;
        foreach (var value in scale.GetMarks(interval))
        {
            var angle = scale.ToAngle(value);
            var (x0, y0) = geometry.GetPoint(angle, innerExtent);
            var (x1, y1) = geometry.GetPoint(angle, outerExtent);
            canvas.DrawLine(x0, y0, x1, y1, paint);
        }
    }

    private static void DrawLabels(SKCanvas canvas, GaugeGeometry geometry, GaugeScale scale, GaugeRenderState state)
    {
        if (state.LabelInterval <= 0)
        {
            return;
        }

        using var font = new SKFont(SKTypeface.Default, state.LabelFontSize);
        using var paint = new SKPaint();
        paint.IsAntialias = true;
        paint.Color = state.LabelColor.ToSKColor();
        var metrics = font.Metrics;
        foreach (var value in scale.GetMarks(state.LabelInterval))
        {
            var text = FormatLabel(state, value);
            if (String.IsNullOrEmpty(text))
            {
                continue;
            }

            var (x, y) = geometry.GetPoint(scale.ToAngle(value), state.LabelExtent);
            canvas.DrawText(text, GaugeTextLayout.CenterStart(x, font.MeasureText(text)), GaugeTextLayout.CenterBaseline(y, metrics.Ascent, metrics.Descent), SKTextAlign.Left, font, paint);
        }
    }

    //--------------------------------------------------------------------------------
    // Center value
    //--------------------------------------------------------------------------------

    private static void DrawCenterValue(SKCanvas canvas, GaugeGeometry geometry, GaugeRenderState state)
    {
        if (!state.ShowValue)
        {
            return;
        }

        var x = geometry.CenterX;
        var y = geometry.CenterY + (geometry.Radius * state.ValueOffsetY);
        var valueText = FormatValue(state);
        using var valueFont = new SKFont(SKTypeface.Default, state.ValueFontSize);
        using var valuePaint = new SKPaint();
        valuePaint.IsAntialias = true;
        valuePaint.Color = state.ValueColor.ToSKColor();
        var valueMetrics = valueFont.Metrics;
        var valueWidth = valueFont.MeasureText(valueText);
        var unit = state.Unit;
        if (String.IsNullOrEmpty(unit))
        {
            canvas.DrawText(valueText, GaugeTextLayout.CenterStart(x, valueWidth), GaugeTextLayout.CenterBaseline(y, valueMetrics.Ascent, valueMetrics.Descent), SKTextAlign.Left, valueFont, valuePaint);
            return;
        }

        using var unitFont = new SKFont(SKTypeface.Default, state.UnitFontSize);
        using var unitPaint = new SKPaint();
        unitPaint.IsAntialias = true;
        unitPaint.Color = state.UnitColor.ToSKColor();
        var unitMetrics = unitFont.Metrics;
        var unitWidth = unitFont.MeasureText(unit);
        if (state.UnitPosition == GaugeUnitPosition.Bottom)
        {
            var (valueBaseline, unitBaseline) = GaugeTextLayout.StackBaselines(y, valueMetrics.Ascent, valueMetrics.Descent, unitMetrics.Ascent, unitMetrics.Descent);
            canvas.DrawText(valueText, GaugeTextLayout.CenterStart(x, valueWidth), valueBaseline, SKTextAlign.Left, valueFont, valuePaint);
            canvas.DrawText(unit, GaugeTextLayout.CenterStart(x, unitWidth), unitBaseline, SKTextAlign.Left, unitFont, unitPaint);
        }
        else
        {
            var (valueStart, unitStart) = GaugeTextLayout.RowStarts(x, valueWidth, unitWidth);
            canvas.DrawText(valueText, valueStart, GaugeTextLayout.CenterBaseline(y, valueMetrics.Ascent, valueMetrics.Descent), SKTextAlign.Left, valueFont, valuePaint);
            canvas.DrawText(unit, unitStart, GaugeTextLayout.CenterBaseline(y, unitMetrics.Ascent, unitMetrics.Descent), SKTextAlign.Left, unitFont, unitPaint);
        }
    }

    //--------------------------------------------------------------------------------
    // Needle
    //--------------------------------------------------------------------------------

    private static void DrawNeedle(SKCanvas canvas, GaugeGeometry geometry, GaugeScale scale, GaugeRenderState state)
    {
        var angle = scale.ToAngle((float)Math.Clamp(state.DisplayValue, state.MinValue, state.MaxValue));
        var endRadius = geometry.Radius * state.NeedleEndExtent;
        var startHalfWidth = state.NeedleStartWidth / 2f;
        var endHalfWidth = state.NeedleEndWidth / 2f;

        canvas.Save();
        canvas.Translate(geometry.CenterX, geometry.CenterY);
        canvas.RotateDegrees(angle);

        using var leftBuilder = new SKPathBuilder();
        leftBuilder.MoveTo(-startHalfWidth, 0f);
        leftBuilder.LineTo(-endHalfWidth, -endRadius);
        leftBuilder.LineTo(0f, -endRadius);
        leftBuilder.LineTo(0f, 0f);
        leftBuilder.Close();
        using var leftPath = leftBuilder.Detach();
        using var leftPaint = new SKPaint();
        leftPaint.Style = SKPaintStyle.Fill;
        leftPaint.Color = state.NeedleLeftColor.ToSKColor();
        leftPaint.IsAntialias = true;
        canvas.DrawPath(leftPath, leftPaint);

        using var rightBuilder = new SKPathBuilder();
        rightBuilder.MoveTo(0f, 0f);
        rightBuilder.LineTo(0f, -endRadius);
        rightBuilder.LineTo(endHalfWidth, -endRadius);
        rightBuilder.LineTo(startHalfWidth, 0f);
        rightBuilder.Close();
        using var rightPath = rightBuilder.Detach();
        using var rightPaint = new SKPaint();
        rightPaint.Style = SKPaintStyle.Fill;
        rightPaint.Color = state.NeedleRightColor.ToSKColor();
        rightPaint.IsAntialias = true;
        canvas.DrawPath(rightPath, rightPaint);

        canvas.Restore();
    }

    private static void DrawPivot(SKCanvas canvas, GaugeGeometry geometry, GaugeRenderState state)
    {
        var color = state.PivotColor.ToSKColor();
        var radius = state.PivotRadius;
        var cx = geometry.CenterX;
        var cy = geometry.CenterY;

        using var shader = SKShader.CreateRadialGradient(new SKPoint(cx - (radius * 0.25f), cy - (radius * 0.25f)), radius * 1.1f, [GetPivotHighlight(color), color], null, SKShaderTileMode.Clamp);
        using var paint = new SKPaint();
        paint.Style = SKPaintStyle.Fill;
        paint.IsAntialias = true;
        paint.Shader = shader;
        canvas.DrawCircle(cx, cy, radius, paint);
    }
}
