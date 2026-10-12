namespace ClamGauge.Tests;

using System.Reflection;

public sealed class ClamGaugeViewTests
{
    private static readonly (string Name, Type Type, object? Default)[] Defaults =
    [
        (nameof(ClamGaugeView.Value), typeof(double), 0d),
        (nameof(ClamGaugeView.MinValue), typeof(double), 0d),
        (nameof(ClamGaugeView.MaxValue), typeof(double), 100d),
        (nameof(ClamGaugeView.StartAngle), typeof(double), 225d),
        (nameof(ClamGaugeView.SweepAngle), typeof(double), 270d),
        (nameof(ClamGaugeView.FontFamily), typeof(string), "sans-serif"),
        (nameof(ClamGaugeView.GaugeBackgroundStartColor), typeof(Color), Colors.Transparent),
        (nameof(ClamGaugeView.GaugeBackgroundEndColor), typeof(Color), Colors.Transparent),
        (nameof(ClamGaugeView.BezelOuterThickness), typeof(double), 4d),
        (nameof(ClamGaugeView.BezelOuterStartColor), typeof(Color), Color.FromArgb("#E8EEF8")),
        (nameof(ClamGaugeView.BezelOuterEndColor), typeof(Color), Color.FromArgb("#6878A0")),
        (nameof(ClamGaugeView.BezelInnerThickness), typeof(double), 2d),
        (nameof(ClamGaugeView.BezelInnerStartColor), typeof(Color), Color.FromArgb("#4A5A88")),
        (nameof(ClamGaugeView.BezelInnerEndColor), typeof(Color), Color.FromArgb("#C0D0E8")),
        (nameof(ClamGaugeView.ArcInnerExtent), typeof(double), 0.77d),
        (nameof(ClamGaugeView.ArcOuterExtent), typeof(double), 0.87d),
        (nameof(ClamGaugeView.ArcTrackColor), typeof(Color), Color.FromArgb("#20000000")),
        (nameof(ClamGaugeView.ArcStartColor), typeof(Color), Colors.DodgerBlue),
        (nameof(ClamGaugeView.ArcEndColor), typeof(Color), Colors.DodgerBlue),
        (nameof(ClamGaugeView.MajorTickInterval), typeof(double), 10d),
        (nameof(ClamGaugeView.MajorTickInnerExtent), typeof(double), 0.68d),
        (nameof(ClamGaugeView.MajorTickOuterExtent), typeof(double), 0.76d),
        (nameof(ClamGaugeView.MajorTickThickness), typeof(double), 2d),
        (nameof(ClamGaugeView.MajorTickColor), typeof(Color), Colors.Black),
        (nameof(ClamGaugeView.MinorTickInterval), typeof(double), 2d),
        (nameof(ClamGaugeView.MinorTickInnerExtent), typeof(double), 0.72d),
        (nameof(ClamGaugeView.MinorTickOuterExtent), typeof(double), 0.76d),
        (nameof(ClamGaugeView.MinorTickThickness), typeof(double), 1d),
        (nameof(ClamGaugeView.MinorTickColor), typeof(Color), Colors.DimGray),
        (nameof(ClamGaugeView.LabelInterval), typeof(double), 20d),
        (nameof(ClamGaugeView.LabelExtent), typeof(double), 0.57d),
        (nameof(ClamGaugeView.LabelFormat), typeof(string), "{0:N0}"),
        (nameof(ClamGaugeView.LabelConverter), typeof(IValueConverter), null),
        (nameof(ClamGaugeView.LabelFontSize), typeof(double), 12d),
        (nameof(ClamGaugeView.LabelColor), typeof(Color), Colors.Black),
        (nameof(ClamGaugeView.ValueVisible), typeof(bool), true),
        (nameof(ClamGaugeView.ValueFormat), typeof(string), "{0:N1}"),
        (nameof(ClamGaugeView.ValueFontSize), typeof(double), 28d),
        (nameof(ClamGaugeView.ValueColor), typeof(Color), Colors.Black),
        (nameof(ClamGaugeView.Unit), typeof(string), null),
        (nameof(ClamGaugeView.UnitFontSize), typeof(double), 12d),
        (nameof(ClamGaugeView.UnitColor), typeof(Color), Colors.DimGray),
        (nameof(ClamGaugeView.UnitPosition), typeof(GaugeUnitPosition), GaugeUnitPosition.Bottom),
        (nameof(ClamGaugeView.ValueOffsetY), typeof(double), 0.3d),
        (nameof(ClamGaugeView.NeedleEndExtent), typeof(double), 0.75d),
        (nameof(ClamGaugeView.NeedleStartWidth), typeof(double), 10d),
        (nameof(ClamGaugeView.NeedleEndWidth), typeof(double), 2d),
        (nameof(ClamGaugeView.NeedleLeftColor), typeof(Color), Color.FromArgb("#CC2828")),
        (nameof(ClamGaugeView.NeedleRightColor), typeof(Color), Color.FromArgb("#EE3C3C")),
        (nameof(ClamGaugeView.PivotRadius), typeof(double), 8d),
        (nameof(ClamGaugeView.PivotColor), typeof(Color), Color.FromArgb("#CC2828")),
        (nameof(ClamGaugeView.AnimationDuration), typeof(int), 300),
        (nameof(ClamGaugeView.AnimationEasing), typeof(Easing), Easing.CubicOut)
    ];

    [Fact]
    public void BindablePropertiesKeepTheirNamesTypesAndDefaults()
    {
        // Arrange
        using var view = new ClamGaugeView();
        var properties = GetBindableProperties();

        // Assert
        Assert.Equal(Defaults.Length + 1, properties.Count);
        foreach (var (name, type, expected) in Defaults)
        {
            AssertProperty(properties[name], name, type);
            Assert.Equal(expected, view.GetValue(properties[name]));
        }

        AssertProperty(properties[nameof(ClamGaugeView.Ranges)], nameof(ClamGaugeView.Ranges), typeof(IList<GaugeRange>));
    }

    [Fact]
    public void RangesStartAsAnEmptyObservableCollectionOfEachView()
    {
        // Arrange
        using var first = new ClamGaugeView();
        using var second = new ClamGaugeView();

        // Act
        first.Ranges.Add(new GaugeRange { StartValue = 0, EndValue = 10, StartColor = Colors.Red });

        // Assert
        Assert.IsType<ObservableCollection<GaugeRange>>(first.Ranges);
        Assert.Single(first.Ranges);
        Assert.Empty(second.Ranges);
    }

    [Fact]
    public void RangesCanBeReplaced()
    {
        // Arrange
        using var view = new ClamGaugeView();
        GaugeRange[] ranges = [new() { StartValue = 950, EndValue = 1050, StartColor = Color.FromArgb("#FFB0B0"), EndColor = Color.FromArgb("#CC0000") }];
        var observable = new ObservableCollection<GaugeRange>();

        // Act
        view.SetValue(ClamGaugeView.RangesProperty, ranges);

        // Assert
        Assert.Same(ranges, view.Ranges);
        Assert.Same(ranges, view.CreateRenderState().Ranges);

        // Act
        view.Ranges = observable;

        // Assert
        Assert.Same(observable, view.Ranges);
        Assert.Same(observable, view.CreateRenderState().Ranges);
    }

    [Fact]
    public void ValueIsShownAtOnceWhileTheViewIsNotAttached()
    {
        // Act
        using var view = new ClamGaugeView { Value = 40 };

        // Assert
        Assert.Equal(40, view.DisplayValue);

        // Act
        view.Value = 75;

        // Assert
        Assert.Equal(75, view.DisplayValue);
        Assert.Equal(75, view.CreateRenderState().DisplayValue);
    }

    [Fact]
    public void DerivedViewsCanExtendDisposeAndDisposingTwiceIsSafe()
    {
        // Arrange
        var view = new DisposeCountingView { Value = 10 };

        // Act
        view.Dispose();
        view.Dispose();
        view.Value = 20;

        // Assert
        Assert.Equal(2, view.DisposeCalls);
        Assert.Equal(20, view.DisplayValue);
    }

    [Fact]
    public void FormatsAreParsedOnceUntilTheyChange()
    {
        // Arrange
        using var view = new ClamGaugeView();

        // Act
        var first = view.CreateRenderState();
        var second = view.CreateRenderState();
        view.LabelFormat = "N2";
        view.ValueFormat = "{0:N0} km/h";
        var changed = view.CreateRenderState();

        // Assert
        Assert.Same(first.LabelFormat, second.LabelFormat);
        Assert.Same(first.ValueFormat, second.ValueFormat);
        Assert.NotSame(first.LabelFormat, changed.LabelFormat);
        Assert.NotSame(first.ValueFormat, changed.ValueFormat);
        Assert.Equal("1.50", changed.LabelFormat.Format(1.5, CultureInfo.InvariantCulture));
        Assert.Equal("60 km/h", changed.ValueFormat.Format(60d, CultureInfo.InvariantCulture));
    }

    [Fact]
    public void RenderStateCopiesEveryVisualProperty()
    {
        // Arrange
        using var view = new ClamGaugeView();
        var expected = new Dictionary<string, object?>();
        var index = 0;
        foreach (var (name, type, _) in Defaults)
        {
            index++;

            if (name is nameof(ClamGaugeView.Value) or nameof(ClamGaugeView.FontFamily) or nameof(ClamGaugeView.LabelFormat) or nameof(ClamGaugeView.ValueFormat) or nameof(ClamGaugeView.AnimationDuration) or nameof(ClamGaugeView.AnimationEasing))
            {
                continue;
            }

            var value = CreateDistinctValue(type, index);
            view.SetValue(GetBindableProperties()[name], value);
            var stateType = typeof(GaugeRenderState).GetProperty(name)!.PropertyType;
            expected[name] = ((value is double number) && (stateType == typeof(float))) ? (float)number : value;
        }

        Func<double, string> formatter = static value => value.ToString(CultureInfo.InvariantCulture);
        view.LabelFormatter = formatter;

        // Act
        var state = view.CreateRenderState();

        // Assert
        foreach (var (name, value) in expected)
        {
            Assert.Equal(value, typeof(GaugeRenderState).GetProperty(name)!.GetValue(state));
        }

        Assert.Same(formatter, state.LabelFormatter);
        Assert.Same(view.Ranges, state.Ranges);
    }

    [Fact]
    public void DoubleValuesFromCodeResourcesAndStylesAreApplied()
    {
        // Arrange
        using var view = new ClamGaugeView
        {
            Resources = new ResourceDictionary { ["LabelSize"] = 22d }
        };

        // Act
        view.SetValue(ClamGaugeView.ValueFontSizeProperty, 14d);
        view.SetDynamicResource(ClamGaugeView.LabelFontSizeProperty, "LabelSize");
        view.Style = new Style(typeof(ClamGaugeView))
        {
            Setters = { new Setter { Property = ClamGaugeView.UnitFontSizeProperty, Value = 24d } }
        };
        var state = view.CreateRenderState();

        // Assert
        Assert.Equal(14d, view.ValueFontSize);
        Assert.Equal(22d, view.LabelFontSize);
        Assert.Equal(24d, view.UnitFontSize);
        Assert.Equal(14f, state.ValueFontSize);
        Assert.Equal(22f, state.LabelFontSize);
        Assert.Equal(24f, state.UnitFontSize);
    }

    [Theory]
    [InlineData(nameof(ClamGaugeView.StartAngle))]
    [InlineData(nameof(ClamGaugeView.SweepAngle))]
    [InlineData(nameof(ClamGaugeView.ValueOffsetY))]
    public void AnglesAndOffsetAcceptOnlyFiniteValues(string name)
    {
        // Arrange
        using var view = new ClamGaugeView();
        var property = GetBindableProperties()[name];

        // Act
        view.SetValue(property, ConvertTo(property, -90));

        // Assert
        Assert.Equal(ConvertTo(property, -90), view.GetValue(property));
        AssertRejected(view, property, Double.NaN, Double.PositiveInfinity, Double.NegativeInfinity);
    }

    [Theory]
    [InlineData(nameof(ClamGaugeView.LabelFontSize))]
    [InlineData(nameof(ClamGaugeView.ValueFontSize))]
    [InlineData(nameof(ClamGaugeView.UnitFontSize))]
    public void FontSizesAcceptOnlyPositiveValues(string name)
    {
        // Arrange
        using var view = new ClamGaugeView();
        var property = GetBindableProperties()[name];

        // Act
        view.SetValue(property, ConvertTo(property, 0.5));

        // Assert
        Assert.Equal(ConvertTo(property, 0.5), view.GetValue(property));
        AssertRejected(view, property, 0, -1, Double.NaN, Double.PositiveInfinity);
    }

    [Theory]
    [InlineData(nameof(ClamGaugeView.BezelOuterThickness))]
    [InlineData(nameof(ClamGaugeView.BezelInnerThickness))]
    [InlineData(nameof(ClamGaugeView.ArcInnerExtent))]
    [InlineData(nameof(ClamGaugeView.ArcOuterExtent))]
    [InlineData(nameof(ClamGaugeView.MajorTickInterval))]
    [InlineData(nameof(ClamGaugeView.MajorTickInnerExtent))]
    [InlineData(nameof(ClamGaugeView.MajorTickOuterExtent))]
    [InlineData(nameof(ClamGaugeView.MajorTickThickness))]
    [InlineData(nameof(ClamGaugeView.MinorTickInterval))]
    [InlineData(nameof(ClamGaugeView.MinorTickInnerExtent))]
    [InlineData(nameof(ClamGaugeView.MinorTickOuterExtent))]
    [InlineData(nameof(ClamGaugeView.MinorTickThickness))]
    [InlineData(nameof(ClamGaugeView.LabelInterval))]
    [InlineData(nameof(ClamGaugeView.LabelExtent))]
    [InlineData(nameof(ClamGaugeView.NeedleEndExtent))]
    [InlineData(nameof(ClamGaugeView.NeedleStartWidth))]
    [InlineData(nameof(ClamGaugeView.NeedleEndWidth))]
    [InlineData(nameof(ClamGaugeView.PivotRadius))]
    public void SizesAndIntervalsAcceptZeroButNotNegativeValues(string name)
    {
        // Arrange
        using var view = new ClamGaugeView();
        var property = GetBindableProperties()[name];

        // Act
        view.SetValue(property, ConvertTo(property, 0));

        // Assert
        Assert.Equal(ConvertTo(property, 0), view.GetValue(property));
        AssertRejected(view, property, -1, Double.NaN, Double.PositiveInfinity);
    }

    [Fact]
    public void AnimationDurationAcceptsZeroButNotNegativeValues()
    {
        // Arrange
        using var view = new ClamGaugeView();

        // Act
        view.AnimationDuration = 0;

        // Assert
        Assert.Equal(0, view.AnimationDuration);
        Assert.Throws<ArgumentOutOfRangeException>(() => view.AnimationDuration = -1);
        Assert.Equal(0, view.AnimationDuration);
    }

    private static object? ConvertTo(BindableProperty property, double value) =>
        Convert.ChangeType(value, property.ReturnType, CultureInfo.InvariantCulture);

    private static void AssertRejected(ClamGaugeView view, BindableProperty property, params double[] values)
    {
        var current = view.GetValue(property);
        foreach (var value in values)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => view.SetValue(property, ConvertTo(property, value)));
            Assert.Equal(current, view.GetValue(property));
        }
    }

    private static void AssertProperty(BindableProperty property, string name, Type type)
    {
        var clrProperty = typeof(ClamGaugeView).GetProperty(name)!;
        Assert.Equal(name, property.PropertyName);
        Assert.Equal(type, property.ReturnType);
        Assert.Equal(type, clrProperty.PropertyType);
        Assert.True(clrProperty.CanRead && clrProperty.CanWrite);
    }

    private static Dictionary<string, BindableProperty> GetBindableProperties() =>
        typeof(ClamGaugeView).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(static field => field.FieldType == typeof(BindableProperty))
            .Select(static field => (BindableProperty)field.GetValue(null)!)
            .ToDictionary(static property => property.PropertyName);

    private static object CreateDistinctValue(Type type, int index) => type switch
    {
        _ when type == typeof(double) => 1000d + index,
        _ when type == typeof(bool) => false,
        _ when type == typeof(string) => $"{{0}} {index}",
        _ when type == typeof(Color) => new Color(index / 100f, 0.5f, 0.25f, 0.75f),
        _ when type == typeof(GaugeUnitPosition) => GaugeUnitPosition.Right,
        _ when type == typeof(IValueConverter) => new LabelConverter(),
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };

    private sealed class LabelConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => String.Empty;

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();
    }

    private sealed class DisposeCountingView : ClamGaugeView
    {
        public int DisposeCalls { get; private set; }

        protected override void Dispose(bool disposing)
        {
            DisposeCalls++;
            base.Dispose(disposing);
        }
    }
}
