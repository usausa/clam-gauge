namespace ClamGauge.Tests;

using System.Reflection;

public sealed class ClamGaugeViewTests
{
    private static readonly (string Name, Type Type, object? Default)[] Defaults =
    [
        (nameof(ClamGaugeView.Value), typeof(double), 0d),
        (nameof(ClamGaugeView.MinValue), typeof(double), 0d),
        (nameof(ClamGaugeView.MaxValue), typeof(double), 100d),
        (nameof(ClamGaugeView.StartAngle), typeof(float), 225f),
        (nameof(ClamGaugeView.SweepAngle), typeof(float), 270f),
        (nameof(ClamGaugeView.FontFamily), typeof(string), "sans-serif"),
        (nameof(ClamGaugeView.GaugeBackgroundColor), typeof(Color), Colors.Transparent),
        (nameof(ClamGaugeView.GaugeBackgroundColor2), typeof(Color), Colors.Transparent),
        (nameof(ClamGaugeView.BezelOuterThickness), typeof(float), 4f),
        (nameof(ClamGaugeView.BezelOuterStartColor), typeof(Color), Color.FromArgb("#E8EEF8")),
        (nameof(ClamGaugeView.BezelOuterEndColor), typeof(Color), Color.FromArgb("#6878A0")),
        (nameof(ClamGaugeView.BezelInnerThickness), typeof(float), 2f),
        (nameof(ClamGaugeView.BezelInnerStartColor), typeof(Color), Color.FromArgb("#4A5A88")),
        (nameof(ClamGaugeView.BezelInnerEndColor), typeof(Color), Color.FromArgb("#C0D0E8")),
        (nameof(ClamGaugeView.ArcInnerExtent), typeof(float), 0.77f),
        (nameof(ClamGaugeView.ArcOuterExtent), typeof(float), 0.87f),
        (nameof(ClamGaugeView.ArcTrackColor), typeof(Color), Color.FromArgb("#20000000")),
        (nameof(ClamGaugeView.ArcStartColor), typeof(Color), Colors.DodgerBlue),
        (nameof(ClamGaugeView.ArcEndColor), typeof(Color), Colors.DodgerBlue),
        (nameof(ClamGaugeView.MajorTickInterval), typeof(double), 10d),
        (nameof(ClamGaugeView.MajorTickInnerExtent), typeof(float), 0.68f),
        (nameof(ClamGaugeView.MajorTickOuterExtent), typeof(float), 0.76f),
        (nameof(ClamGaugeView.MajorTickThickness), typeof(float), 2f),
        (nameof(ClamGaugeView.MajorTickColor), typeof(Color), Colors.Black),
        (nameof(ClamGaugeView.MinorTickInterval), typeof(double), 2d),
        (nameof(ClamGaugeView.MinorTickInnerExtent), typeof(float), 0.72f),
        (nameof(ClamGaugeView.MinorTickOuterExtent), typeof(float), 0.76f),
        (nameof(ClamGaugeView.MinorTickThickness), typeof(float), 1f),
        (nameof(ClamGaugeView.MinorTickColor), typeof(Color), Colors.DimGray),
        (nameof(ClamGaugeView.LabelInterval), typeof(double), 20d),
        (nameof(ClamGaugeView.LabelExtent), typeof(float), 0.57f),
        (nameof(ClamGaugeView.LabelFormat), typeof(string), "{0:N0}"),
        (nameof(ClamGaugeView.LabelConverter), typeof(IValueConverter), null),
        (nameof(ClamGaugeView.LabelFontSize), typeof(float), 12f),
        (nameof(ClamGaugeView.LabelColor), typeof(Color), Colors.Black),
        (nameof(ClamGaugeView.ShowValue), typeof(bool), true),
        (nameof(ClamGaugeView.ValueFormat), typeof(string), "{0:N1}"),
        (nameof(ClamGaugeView.ValueFontSize), typeof(float), 28f),
        (nameof(ClamGaugeView.ValueColor), typeof(Color), Colors.Black),
        (nameof(ClamGaugeView.Unit), typeof(string), null),
        (nameof(ClamGaugeView.UnitFontSize), typeof(float), 12f),
        (nameof(ClamGaugeView.UnitColor), typeof(Color), Colors.DimGray),
        (nameof(ClamGaugeView.UnitPosition), typeof(GaugeUnitPosition), GaugeUnitPosition.Bottom),
        (nameof(ClamGaugeView.ValueOffsetY), typeof(float), 0.3f),
        (nameof(ClamGaugeView.NeedleEndExtent), typeof(float), 0.75f),
        (nameof(ClamGaugeView.NeedleStartWidth), typeof(float), 10f),
        (nameof(ClamGaugeView.NeedleEndWidth), typeof(float), 2f),
        (nameof(ClamGaugeView.NeedleLeftColor), typeof(Color), Color.FromArgb("#CC2828")),
        (nameof(ClamGaugeView.NeedleRightColor), typeof(Color), Color.FromArgb("#EE3C3C")),
        (nameof(ClamGaugeView.PivotRadius), typeof(float), 8f),
        (nameof(ClamGaugeView.PivotColor), typeof(Color), Color.FromArgb("#CC2828")),
        (nameof(ClamGaugeView.AnimationDuration), typeof(int), 300),
        (nameof(ClamGaugeView.AnimationEasing), typeof(Easing), Easing.CubicOut)
    ];

    [Fact]
    public void BindablePropertiesKeepTheirNamesTypesAndDefaults()
    {
        // Arrange
        var view = new ClamGaugeView();
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
        var first = new ClamGaugeView();
        var second = new ClamGaugeView();

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
        var view = new ClamGaugeView();
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
        var view = new ClamGaugeView { Value = 40 };

        // Assert
        Assert.Equal(40, view.DisplayValue);

        // Act
        view.Value = 75;

        // Assert
        Assert.Equal(75, view.DisplayValue);
        Assert.Equal(75, view.CreateRenderState().DisplayValue);
    }

    [Fact]
    public void RenderStateCopiesEveryVisualProperty()
    {
        // Arrange
        var view = new ClamGaugeView();
        var expected = new Dictionary<string, object?>();
        var index = 0;
        foreach (var (name, type, _) in Defaults)
        {
            index++;

            if (name is nameof(ClamGaugeView.Value) or nameof(ClamGaugeView.FontFamily) or nameof(ClamGaugeView.AnimationDuration) or nameof(ClamGaugeView.AnimationEasing))
            {
                continue;
            }

            var value = CreateDistinctValue(type, index);
            view.SetValue(GetBindableProperties()[name], value);
            expected[name] = value;
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
        _ when type == typeof(float) => index + 0.5f,
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
}
