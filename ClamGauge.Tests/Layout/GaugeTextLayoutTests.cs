namespace ClamGauge.Tests.Layout;

public sealed class GaugeTextLayoutTests
{
    [Fact]
    public void UnitBelowTheValueIsCenteredAsABlock()
    {
        // Act
        var (value, unit) = GaugeTextLayout.StackBaselines(100, -20, 5, -8, 2);

        // Assert
        Assert.Equal(101.5f, value);
        Assert.Equal(116.5f, unit);
        Assert.Equal(value + 5 + GaugeTextLayout.UnitBottomGap, unit - 8);
    }

    [Fact]
    public void UnitRightOfTheValueIsCenteredAsARow()
    {
        // Act
        var (value, unit) = GaugeTextLayout.RowStarts(200, 60, 20);

        // Assert
        Assert.Equal(158, value);
        Assert.Equal(value + 60 + GaugeTextLayout.UnitRightGap, unit);
        Assert.Equal(200, (value + unit + 20) / 2);
    }
}
