namespace ClamGauge.Tests.Rendering;

using SkiaSharp;
using SkiaSharp.Views.Maui;

public sealed class GaugeRendererTests
{
    private const int Size = 300;

    [Fact]
    public void LabelsPreferTheFormatterThenTheConverterThenTheFormat()
    {
        // Arrange
        var view = new ClamGaugeView { LabelFormat = "{0:N0}%" };

        // Act & Assert
        Assert.Equal("40%", GaugeRenderer.FormatLabel(view.CreateRenderState(), 40));

        // Act & Assert
        view.LabelConverter = new CompassConverter();
        Assert.Equal("E", GaugeRenderer.FormatLabel(view.CreateRenderState(), 90));
        Assert.Equal(String.Empty, GaugeRenderer.FormatLabel(view.CreateRenderState(), 45));

        // Act & Assert
        view.LabelFormatter = static value => $"<{value.ToString(CultureInfo.InvariantCulture)}>";
        Assert.Equal("<90>", GaugeRenderer.FormatLabel(view.CreateRenderState(), 90));
    }

    [Fact]
    public void CenterValueIsTheDisplayedValueInTheFormat()
    {
        // Arrange
        var culture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        try
        {
            var view = new ClamGaugeView { MaxValue = 2000, Value = 1013.4 };

            // Act & Assert
            Assert.Equal("1,013.4", GaugeRenderer.FormatValue(view.CreateRenderState()));

            // Act & Assert
            view.ValueFormat = "{0:N0}";
            Assert.Equal("1,013", GaugeRenderer.FormatValue(view.CreateRenderState()));
        }
        finally
        {
            CultureInfo.CurrentCulture = culture;
        }
    }

    [Fact]
    public void PivotHighlightIsLighterAndOpaque()
    {
        // Act & Assert
        Assert.Equal(new SKColor(255, 90, 80), GaugeRenderer.GetPivotHighlight(new SKColor(0xCC, 0x28, 0x28)));
        Assert.Equal(new SKColor(80, 50, 40), GaugeRenderer.GetPivotHighlight(new SKColor(0, 0, 0, 128)));
    }

    [Fact]
    public void RenderDrawsEveryPartWithoutError()
    {
        // Arrange
        var views = new[]
        {
            new ClamGaugeView { Value = 60, GaugeBackgroundColor = Colors.Navy, GaugeBackgroundColor2 = Colors.White, BezelInnerThickness = 3, Unit = "km/h" },
            new ClamGaugeView { Value = 30, GaugeBackgroundColor = Colors.Black, Unit = "%", UnitPosition = GaugeUnitPosition.Right, LabelFormat = "{0:N0}%" },
            new ClamGaugeView { Value = 270, MaxValue = 360, StartAngle = 0, SweepAngle = 360, ShowValue = false, LabelInterval = 90, LabelConverter = new CompassConverter(), AnimationDuration = 0 },
            new ClamGaugeView { Value = 7, MaxValue = 8, StartAngle = -150, MinorTickInterval = 0.25, MajorTickInterval = 1, LabelInterval = 2, LabelFormatter = static value => $"{value:N0}k" }
        };
        views[0].Ranges.Add(new GaugeRange { StartValue = 0, EndValue = 80, StartColor = Colors.Green, EndColor = Colors.Yellow });
        views[0].Ranges.Add(new GaugeRange { StartValue = 80, EndValue = 140, StartColor = Colors.Orange });
        views[0].Ranges.Add(new GaugeRange { StartValue = 140, EndValue = 400, StartColor = Colors.Red });

        // Act & Assert
        foreach (var view in views)
        {
            using var bitmap = Render(view);
            Assert.NotEqual(0, bitmap.GetPixel(Size / 2, Size / 2).Alpha);
        }

        using var renderer = new GaugeRenderer("sans-serif");
        using var surface = SKSurface.Create(new SKImageInfo(Size, Size));
        renderer.Render(surface.Canvas, 0, 0, views[0].CreateRenderState());
        surface.Canvas.Scale(2.625f);
        renderer.Render(surface.Canvas, Size / 2.625f, Size / 2.625f, views[1].CreateRenderState());
    }

    [Fact]
    public void NothingIsDrawnWithoutAScaleOrARadius()
    {
        // Arrange
        var view = new ClamGaugeView { Value = 50, GaugeBackgroundColor = Colors.Black };
        using var renderer = new GaugeRenderer(view.FontFamily);
        using var surface = SKSurface.Create(new SKImageInfo(Size, Size));
        renderer.Render(surface.Canvas, Size, Size, view.CreateRenderState());

        // Act
        view.MaxValue = view.MinValue;
        renderer.Render(surface.Canvas, Size, Size, view.CreateRenderState());

        // Assert
        Assert.True(IsBlank(surface));

        // Act
        view.MaxValue = 100;
        view.BezelOuterThickness = Size;
        renderer.Render(surface.Canvas, Size, Size, view.CreateRenderState());

        // Assert
        Assert.True(IsBlank(surface));
    }

    [Fact]
    public void TextIsShapedByTheFontResolverOnceForRepeatedFrames()
    {
        // Arrange
        var view = new ClamGaugeView { Value = 60, Unit = "回転/分", LabelFormatter = static value => $"{value:N0}℃" };
        using var renderer = new GaugeRenderer(view.FontFamily);
        using var surface = SKSurface.Create(new SKImageInfo(Size, Size));

        // Act
        renderer.Render(surface.Canvas, Size, Size, view.CreateRenderState());
        var shaped = renderer.Measurements;
        renderer.Render(surface.Canvas, Size, Size, view.CreateRenderState());

        // Assert
        Assert.True(shaped > 0);
        Assert.Equal(shaped, renderer.Measurements);
    }

    [Fact]
    public void BackgroundFillsTheDialAndLeavesTheCornersTransparent()
    {
        // Arrange
        var background = Color.FromArgb("#123456");
        var view = new ClamGaugeView { GaugeBackgroundColor = background };

        // Act
        using var bitmap = Render(view);

        // Assert
        Assert.Equal(background.ToSKColor(), Pixel(bitmap, 180, 0.9f));
        Assert.Equal(0, bitmap.GetPixel(2, 2).Alpha);
    }

    [Fact]
    public void GradientBackgroundRunsFromTheTopLeft()
    {
        // Arrange
        var view = new ClamGaugeView
        {
            Value = 50,
            GaugeBackgroundColor = Colors.Black,
            GaugeBackgroundColor2 = Colors.White,
            MajorTickInterval = 0,
            MinorTickInterval = 0,
            LabelInterval = 0,
            ShowValue = false
        };

        // Act
        using var bitmap = Render(view);

        // Assert
        Assert.True(Pixel(bitmap, 315, 0.3f).Red < Pixel(bitmap, 135, 0.3f).Red);
    }

    [Fact]
    public void RangesReplaceTheArcFill()
    {
        // Arrange
        var view = new ClamGaugeView { ArcTrackColor = Colors.Transparent };
        var scale = new GaugeScale(view.MinValue, view.MaxValue, view.StartAngle, view.SweepAngle);
        var arc = (view.ArcInnerExtent + view.ArcOuterExtent) / 2;

        // Act
        using var filled = Render(view);

        // Assert
        Assert.Equal(Colors.DodgerBlue.ToSKColor(), Pixel(filled, scale.ToAngle(75), arc));

        // Act
        view.Ranges.Add(new GaugeRange { StartValue = 0, EndValue = 50, StartColor = Colors.Red });
        using var ranged = Render(view);

        // Assert
        Assert.Equal(Colors.Red.ToSKColor(), Pixel(ranged, scale.ToAngle(25), arc));
        Assert.Equal(0, Pixel(ranged, scale.ToAngle(75), arc).Alpha);
    }

    [Fact]
    public void NeedleHalvesHaveTheirOwnColors()
    {
        // Arrange
        var view = new ClamGaugeView
        {
            StartAngle = 0,
            SweepAngle = 360,
            ShowValue = false,
            NeedleLeftColor = Colors.Blue,
            NeedleRightColor = Colors.Lime
        };

        // Act
        using var bitmap = Render(view);

        // Assert
        Assert.Equal(Colors.Blue.ToSKColor(), bitmap.GetPixel((Size / 2) - 2, (Size / 2) - 30));
        Assert.Equal(Colors.Blue.ToSKColor(), bitmap.GetPixel((Size / 2) - 1, (Size / 2) - 30));
        Assert.Equal(Colors.Lime.ToSKColor(), bitmap.GetPixel(Size / 2, (Size / 2) - 30));
        Assert.Equal(Colors.Lime.ToSKColor(), bitmap.GetPixel((Size / 2) + 1, (Size / 2) - 30));
    }

    private static SKBitmap Render(ClamGaugeView view)
    {
        using var renderer = new GaugeRenderer(view.FontFamily);
        using var surface = SKSurface.Create(new SKImageInfo(Size, Size));
        renderer.Render(surface.Canvas, Size, Size, view.CreateRenderState());
        using var image = surface.Snapshot();
        return SKBitmap.FromImage(image);
    }

    private static bool IsBlank(SKSurface surface)
    {
        using var image = surface.Snapshot();
        using var bitmap = SKBitmap.FromImage(image);
        return bitmap.Pixels.All(static pixel => pixel.Alpha == 0);
    }

    // Pixel at an angle clockwise from 12 o'clock and an extent of the radius inside the default bezels
    private static SKColor Pixel(SKBitmap bitmap, float angle, float extent)
    {
        var (x, y) = new GaugeGeometry(Size, Size, 4, 2, 0.77f, 0.87f).GetPoint(angle, extent);
        return bitmap.GetPixel((int)x, (int)y);
    }

    private sealed class CompassConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            value is 90d ? "E" : null;

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
