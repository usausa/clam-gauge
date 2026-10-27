namespace ClamGauge;

public partial class ClamGaugeView
{
    public static readonly BindableProperty MajorTickIntervalProperty = BindableProperty.Create(nameof(MajorTickInterval), typeof(double), typeof(ClamGaugeView), 10d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(MajorTickInterval)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MajorTickInnerExtentProperty = BindableProperty.Create(nameof(MajorTickInnerExtent), typeof(double), typeof(ClamGaugeView), 0.68d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(MajorTickInnerExtent)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MajorTickOuterExtentProperty = BindableProperty.Create(nameof(MajorTickOuterExtent), typeof(double), typeof(ClamGaugeView), 0.76d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(MajorTickOuterExtent)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MajorTickThicknessProperty = BindableProperty.Create(nameof(MajorTickThickness), typeof(double), typeof(ClamGaugeView), 2d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(MajorTickThickness)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MajorTickColorProperty = BindableProperty.Create(nameof(MajorTickColor), typeof(Color), typeof(ClamGaugeView), Colors.Black, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickIntervalProperty = BindableProperty.Create(nameof(MinorTickInterval), typeof(double), typeof(ClamGaugeView), 2d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(MinorTickInterval)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickInnerExtentProperty = BindableProperty.Create(nameof(MinorTickInnerExtent), typeof(double), typeof(ClamGaugeView), 0.72d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(MinorTickInnerExtent)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickOuterExtentProperty = BindableProperty.Create(nameof(MinorTickOuterExtent), typeof(double), typeof(ClamGaugeView), 0.76d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(MinorTickOuterExtent)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickThicknessProperty = BindableProperty.Create(nameof(MinorTickThickness), typeof(double), typeof(ClamGaugeView), 1d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(MinorTickThickness)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickColorProperty = BindableProperty.Create(nameof(MinorTickColor), typeof(Color), typeof(ClamGaugeView), Colors.DimGray, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelIntervalProperty = BindableProperty.Create(nameof(LabelInterval), typeof(double), typeof(ClamGaugeView), 20d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(LabelInterval)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelExtentProperty = BindableProperty.Create(nameof(LabelExtent), typeof(double), typeof(ClamGaugeView), 0.57d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(LabelExtent)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelFormatProperty = BindableProperty.Create(nameof(LabelFormat), typeof(string), typeof(ClamGaugeView), "{0:N0}", propertyChanged: OnLabelFormatChanged);
    public static readonly BindableProperty LabelConverterProperty = BindableProperty.Create(nameof(LabelConverter), typeof(IValueConverter), typeof(ClamGaugeView), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelFontSizeProperty = BindableProperty.Create(nameof(LabelFontSize), typeof(double), typeof(ClamGaugeView), 12d, validateValue: static (_, value) => ValidatePositive(value, nameof(LabelFontSize)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(ClamGaugeView), Colors.Black, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelFormatterProperty = BindableProperty.Create(nameof(LabelFormatter), typeof(Func<double, string>), typeof(ClamGaugeView), propertyChanged: OnRenderInputChanged);

    public double MajorTickInterval
    {
        get => (double)GetValue(MajorTickIntervalProperty);
        set => SetValue(MajorTickIntervalProperty, value);
    }

    public double MajorTickInnerExtent
    {
        get => (double)GetValue(MajorTickInnerExtentProperty);
        set => SetValue(MajorTickInnerExtentProperty, value);
    }

    public double MajorTickOuterExtent
    {
        get => (double)GetValue(MajorTickOuterExtentProperty);
        set => SetValue(MajorTickOuterExtentProperty, value);
    }

    public double MajorTickThickness
    {
        get => (double)GetValue(MajorTickThicknessProperty);
        set => SetValue(MajorTickThicknessProperty, value);
    }

    public Color MajorTickColor
    {
        get => (Color)GetValue(MajorTickColorProperty);
        set => SetValue(MajorTickColorProperty, value);
    }

    public double MinorTickInterval
    {
        get => (double)GetValue(MinorTickIntervalProperty);
        set => SetValue(MinorTickIntervalProperty, value);
    }

    public double MinorTickInnerExtent
    {
        get => (double)GetValue(MinorTickInnerExtentProperty);
        set => SetValue(MinorTickInnerExtentProperty, value);
    }

    public double MinorTickOuterExtent
    {
        get => (double)GetValue(MinorTickOuterExtentProperty);
        set => SetValue(MinorTickOuterExtentProperty, value);
    }

    public double MinorTickThickness
    {
        get => (double)GetValue(MinorTickThicknessProperty);
        set => SetValue(MinorTickThicknessProperty, value);
    }

    public Color MinorTickColor
    {
        get => (Color)GetValue(MinorTickColorProperty);
        set => SetValue(MinorTickColorProperty, value);
    }

    // Values between labels; 0 hides the labels
    public double LabelInterval
    {
        get => (double)GetValue(LabelIntervalProperty);
        set => SetValue(LabelIntervalProperty, value);
    }

    public double LabelExtent
    {
        get => (double)GetValue(LabelExtentProperty);
        set => SetValue(LabelExtentProperty, value);
    }

    public string LabelFormat
    {
        get => (string)GetValue(LabelFormatProperty);
        set => SetValue(LabelFormatProperty, value);
    }

    // Converts the label value to its text; used instead of LabelFormat when set
    public IValueConverter? LabelConverter
    {
        get => (IValueConverter?)GetValue(LabelConverterProperty);
        set => SetValue(LabelConverterProperty, value);
    }

    public double LabelFontSize
    {
        get => (double)GetValue(LabelFontSizeProperty);
        set => SetValue(LabelFontSizeProperty, value);
    }

    public Color LabelColor
    {
        get => (Color)GetValue(LabelColorProperty);
        set => SetValue(LabelColorProperty, value);
    }

    public Func<double, string>? LabelFormatter
    {
        get => (Func<double, string>?)GetValue(LabelFormatterProperty);
        set => SetValue(LabelFormatterProperty, value);
    }
}
