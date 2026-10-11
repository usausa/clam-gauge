namespace ClamGauge.Tests;

public sealed class GaugeFontsTests
{
    [Fact]
    public void DefaultsPreferJapaneseGlyphsWithoutExtraTypefaces()
    {
        // Act
        var languages = GaugeFonts.Languages;
        var fallbacks = GaugeFonts.Fallbacks;

        // Assert
        Assert.Equal(["ja"], languages);
        Assert.Empty(fallbacks);
    }

    [Fact]
    public void ResolverShapesMixedScriptsAndTruncatesWithAnEllipsis()
    {
        // Arrange
        using var resolver = new GaugeFontResolver("sans-serif");

        // Act
        var run = resolver.Shape("見出し 🍎 ▲ text", 14, false);
        var truncated = resolver.Shape("A long title that will not fit", 14, false, 40);
        var empty = resolver.Shape(String.Empty, 14, false);
        var tooNarrow = resolver.Shape("abc", 14, false, 1);
        var cached = resolver.Shape("見出し 🍎 ▲ text", 14, false);

        // Assert
        Assert.True(run.Width > 0);
        Assert.NotEmpty(run.Parts);
        Assert.True(resolver.Measurements > 0);
        Assert.Equal("…", truncated.Parts[^1].Text);
        Assert.True(truncated.Width <= 40);
        Assert.Empty(empty.Parts);
        Assert.Empty(tooNarrow.Parts);
        Assert.Same(run, cached);
        Assert.True(resolver.GetLineHeight(14, false) > 0);
    }

    [Fact]
    public void VariationSelectorsAreNotDrawn()
    {
        // Arrange
        using var resolver = new GaugeFontResolver("sans-serif");

        // Act
        var run = resolver.Shape("✈️", 20, false);

        // Assert
        Assert.DoesNotContain(run.Parts, static part => part.Text.Contains('️', StringComparison.Ordinal));
    }

    [Fact]
    public void JapaneseAndEmojiResolveToFontsThatHaveTheGlyphs()
    {
        // Arrange
        using var resolver = new GaugeFontResolver("sans-serif");

        // Act
        var run = resolver.Shape("回転/分 🍎", 12, false);

        // Assert
        Assert.All(run.Parts, static part => Assert.All(part.Text.EnumerateRunes(), rune => Assert.True(part.Font.ContainsGlyph(rune.Value))));
    }
}
