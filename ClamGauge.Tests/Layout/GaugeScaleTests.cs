namespace ClamGauge.Tests.Layout;

public sealed class GaugeScaleTests
{
    [Fact]
    public void ValuesMapLinearlyFromTheStartAngleOverTheSweep()
    {
        // Arrange
        var scale = new GaugeScale(0, 100, 225, 270);
        var tachometer = new GaugeScale(0, 8, -150, 270);

        // Act & Assert
        Assert.Equal(225, scale.ToAngle(0));
        Assert.Equal(360, scale.ToAngle(50));
        Assert.Equal(495, scale.ToAngle(100));
        Assert.Equal(52.5f, tachometer.ToAngle(6));
        Assert.Equal(-90, GaugeScale.ToSkiaAngle(0));
    }

    [Fact]
    public void MarksStepByTheIntervalUpToTheMaximum()
    {
        // Act & Assert
        Assert.Equal([0d, 20, 40, 60, 80, 100], new GaugeScale(0, 100, 225, 270).GetMarks(20));
        Assert.Equal([950d, 970, 990, 1010, 1030, 1050], new GaugeScale(950, 1050, 225, 270).GetMarks(20));
        Assert.Equal([0d, 3, 6, 9], new GaugeScale(0, 10, 225, 270).GetMarks(3));
        Assert.Equal([0d, 6], new GaugeScale(0, 10, 225, 270).GetMarks(6));
        Assert.Equal(33, new GaugeScale(0, 8, -150, 270).GetMarks(0.25).Count);
        Assert.Empty(new GaugeScale(0, 100, 225, 270).GetMarks(0));
        Assert.Empty(new GaugeScale(0, 100, 225, 270).GetMarks(-5));
    }

    [Fact]
    public void LastMarkOvershootingByRoundingIsClampedToTheMaximum()
    {
        // Act
        var marks = new GaugeScale(0, 0.3, 225, 270).GetMarks(0.1);

        // Assert
        Assert.Equal(4, marks.Count);
        Assert.Equal(0.3, marks[^1]);
    }

    [Fact]
    public void RangeArcsAreClampedToTheScale()
    {
        // Arrange
        var scale = new GaugeScale(0, 100, 225, 270);

        // Act & Assert
        Assert.True(scale.TryGetArc(-10, 50, out var start, out var sweep));
        Assert.Equal(225, start);
        Assert.Equal(135, sweep);
        Assert.True(scale.TryGetArc(80, 200, out start, out sweep));
        Assert.Equal(441, start, 3);
        Assert.Equal(54, sweep, 3);
        Assert.False(scale.TryGetArc(60, 40, out _, out _));
        Assert.False(scale.TryGetArc(120, 150, out _, out _));
        Assert.False(new GaugeScale(0, 100, 225, -270).TryGetArc(0, 50, out _, out _));
    }
}
