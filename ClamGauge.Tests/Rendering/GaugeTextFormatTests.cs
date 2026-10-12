namespace ClamGauge.Tests.Rendering;

public sealed class GaugeTextFormatTests
{
    [Fact]
    public void FormatsWithABraceAreCompositeAndOthersAreFormatStrings()
    {
        // Arrange
        var culture = CultureInfo.InvariantCulture;
        var date = new DateTime(2026, 9, 13);

        // Act & Assert
        Assert.Equal("1,234 pcs", GaugeTextFormat.Parse("{0:N0} pcs").Format(1234, culture));
        Assert.Equal("1,234", GaugeTextFormat.Parse("N0").Format(1234, culture));
        Assert.Equal("2026/09", GaugeTextFormat.Parse("yyyy/MM").Format(date, culture));
        Assert.Equal("Yamada san", GaugeTextFormat.Parse("{0} san").Format((object?)"Yamada", culture));
        Assert.Equal("text", GaugeTextFormat.Parse("D6").Format((object?)"text", culture));
        Assert.Equal(String.Empty, GaugeTextFormat.Parse("{0} san").Format(null, culture));
    }

    [Fact]
    public void EmptyFormatsShareTheDefaultText()
    {
        // Arrange
        var culture = CultureInfo.InvariantCulture;

        // Act
        var none = GaugeTextFormat.Parse(null);
        var empty = GaugeTextFormat.Parse(String.Empty);

        // Assert
        Assert.Same(GaugeTextFormat.Default, none);
        Assert.Same(GaugeTextFormat.Default, empty);
        Assert.True(none.IsDefault);
        Assert.False(GaugeTextFormat.Parse("N0").IsDefault);
        Assert.Equal("1234.5", none.Format(1234.5, culture));
        Assert.Equal("1234.5", none.Format((object?)1234.5, culture));
    }

    [Fact]
    public void InvalidCompositeFormatsAreRejectedWhenParsed()
    {
        // Act & Assert
        Assert.Throws<FormatException>(static () => GaugeTextFormat.Parse("{0:N0"));
    }

    [Fact]
    public void FormatsParsedFromTheSameTextAreEqual()
    {
        // Act & Assert
        Assert.Equal(GaugeTextFormat.Parse("{0:N0} pcs"), GaugeTextFormat.Parse("{0:N0} pcs"));
        Assert.Equal(GaugeTextFormat.Parse("{0:N0} pcs").GetHashCode(), GaugeTextFormat.Parse("{0:N0} pcs").GetHashCode());
        Assert.Equal(GaugeTextFormat.Parse("N0"), GaugeTextFormat.Parse("N0"));
        Assert.NotEqual(GaugeTextFormat.Parse("N0"), GaugeTextFormat.Parse("N1"));
        Assert.NotEqual(GaugeTextFormat.Parse("{0:N0}"), GaugeTextFormat.Parse("N0"));
        Assert.NotEqual(GaugeTextFormat.Default, GaugeTextFormat.Parse("N0"));
    }
}
