namespace ClamGauge;

public sealed partial class ClamGaugeView
{
    public static readonly BindableProperty MajorTickIntervalProperty = BindableProperty.Create(nameof(MajorTickInterval), typeof(double), typeof(ClamGaugeView), 10d, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MajorTickInnerExtentProperty = BindableProperty.Create(nameof(MajorTickInnerExtent), typeof(float), typeof(ClamGaugeView), 0.68f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MajorTickOuterExtentProperty = BindableProperty.Create(nameof(MajorTickOuterExtent), typeof(float), typeof(ClamGaugeView), 0.76f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MajorTickThicknessProperty = BindableProperty.Create(nameof(MajorTickThickness), typeof(float), typeof(ClamGaugeView), 2f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MajorTickColorProperty = BindableProperty.Create(nameof(MajorTickColor), typeof(Color), typeof(ClamGaugeView), Colors.Black, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickIntervalProperty = BindableProperty.Create(nameof(MinorTickInterval), typeof(double), typeof(ClamGaugeView), 2d, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickInnerExtentProperty = BindableProperty.Create(nameof(MinorTickInnerExtent), typeof(float), typeof(ClamGaugeView), 0.72f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickOuterExtentProperty = BindableProperty.Create(nameof(MinorTickOuterExtent), typeof(float), typeof(ClamGaugeView), 0.76f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickThicknessProperty = BindableProperty.Create(nameof(MinorTickThickness), typeof(float), typeof(ClamGaugeView), 1f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MinorTickColorProperty = BindableProperty.Create(nameof(MinorTickColor), typeof(Color), typeof(ClamGaugeView), Colors.DimGray, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelIntervalProperty = BindableProperty.Create(nameof(LabelInterval), typeof(double), typeof(ClamGaugeView), 20d, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelExtentProperty = BindableProperty.Create(nameof(LabelExtent), typeof(float), typeof(ClamGaugeView), 0.57f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelFormatProperty = BindableProperty.Create(nameof(LabelFormat), typeof(string), typeof(ClamGaugeView), "{0:N0}", propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelConverterProperty = BindableProperty.Create(nameof(LabelConverter), typeof(IValueConverter), typeof(ClamGaugeView), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelFontSizeProperty = BindableProperty.Create(nameof(LabelFontSize), typeof(float), typeof(ClamGaugeView), 12f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(ClamGaugeView), Colors.Black, propertyChanged: OnRenderInputChanged);

    public double MajorTickInterval
    {
        get => (double)GetValue(MajorTickIntervalProperty);
        set => SetValue(MajorTickIntervalProperty, value);
    }

    public float MajorTickInnerExtent
    {
        get => (float)GetValue(MajorTickInnerExtentProperty);
        set => SetValue(MajorTickInnerExtentProperty, value);
    }

    public float MajorTickOuterExtent
    {
        get => (float)GetValue(MajorTickOuterExtentProperty);
        set => SetValue(MajorTickOuterExtentProperty, value);
    }

    public float MajorTickThickness
    {
        get => (float)GetValue(MajorTickThicknessProperty);
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

    public float MinorTickInnerExtent
    {
        get => (float)GetValue(MinorTickInnerExtentProperty);
        set => SetValue(MinorTickInnerExtentProperty, value);
    }

    public float MinorTickOuterExtent
    {
        get => (float)GetValue(MinorTickOuterExtentProperty);
        set => SetValue(MinorTickOuterExtentProperty, value);
    }

    public float MinorTickThickness
    {
        get => (float)GetValue(MinorTickThicknessProperty);
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

    public float LabelExtent
    {
        get => (float)GetValue(LabelExtentProperty);
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

    public float LabelFontSize
    {
        get => (float)GetValue(LabelFontSizeProperty);
        set => SetValue(LabelFontSizeProperty, value);
    }

    public Color LabelColor
    {
        get => (Color)GetValue(LabelColorProperty);
        set => SetValue(LabelColorProperty, value);
    }

    public Func<double, string>? LabelFormatter
    {
        get;
        set
        {
            field = value;
            InvalidateSurface();
        }
    }
}
