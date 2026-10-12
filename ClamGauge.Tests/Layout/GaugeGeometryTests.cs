namespace ClamGauge.Tests.Layout;

public sealed class GaugeGeometryTests
{
    [Fact]
    public void RadiusIsHalfOfTheShorterSideWithoutTheBezels()
    {
        // Act
        var geometry = new GaugeGeometry(400, 300, 6, 3, 0.8f, 0.9f);

        // Assert
        Assert.Equal(200, geometry.CenterX);
        Assert.Equal(150, geometry.CenterY);
        Assert.Equal(150, geometry.OuterRadius);
        Assert.Equal(141, geometry.Radius);
        Assert.Equal(119.85f, geometry.ArcRadius, 3);
        Assert.Equal(14.1f, geometry.ArcThickness, 3);
    }

    [Fact]
    public void RadiusCanBeEmptyWhenTheBezelsFillTheView()
    {
        // Act
        var geometry = new GaugeGeometry(20, 100, 8, 2, 0.77f, 0.87f);

        // Assert
        Assert.Equal(10, geometry.OuterRadius);
        Assert.Equal(0, geometry.Radius);
    }

    [Fact]
    public void PointsFollowTheAngleClockwiseFromTwelveOClock()
    {
        // Arrange
        var geometry = new GaugeGeometry(200, 200, 0, 0, 0.5f, 0.5f);

        // Act & Assert
        AssertPoint(100, 0, geometry.GetPoint(0, 1));
        AssertPoint(200, 100, geometry.GetPoint(90, 1));
        AssertPoint(100, 150, geometry.GetPoint(180, 0.5f));
        AssertPoint(0, 100, geometry.GetPoint(-90, 1));
        AssertPoint(100, 0, geometry.GetPoint(360, 1));
    }

    private static void AssertPoint(float x, float y, SkiaSharp.SKPoint point)
    {
        Assert.Equal(x, point.X, 3);
        Assert.Equal(y, point.Y, 3);
    }
}
