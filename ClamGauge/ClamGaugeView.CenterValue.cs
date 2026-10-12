namespace ClamGauge;

public partial class ClamGaugeView
{
    public static readonly BindableProperty ValueVisibleProperty = BindableProperty.Create(nameof(ValueVisible), typeof(bool), typeof(ClamGaugeView), true, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ValueFormatProperty = BindableProperty.Create(nameof(ValueFormat), typeof(string), typeof(ClamGaugeView), "{0:N1}", propertyChanged: OnValueFormatChanged);
    public static readonly BindableProperty ValueFontSizeProperty = BindableProperty.Create(nameof(ValueFontSize), typeof(double), typeof(ClamGaugeView), 28d, validateValue: static (_, value) => ValidatePositive(value, nameof(ValueFontSize)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ValueColorProperty = BindableProperty.Create(nameof(ValueColor), typeof(Color), typeof(ClamGaugeView), Colors.Black, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty UnitProperty = BindableProperty.Create(nameof(Unit), typeof(string), typeof(ClamGaugeView), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty UnitFontSizeProperty = BindableProperty.Create(nameof(UnitFontSize), typeof(double), typeof(ClamGaugeView), 12d, validateValue: static (_, value) => ValidatePositive(value, nameof(UnitFontSize)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty UnitColorProperty = BindableProperty.Create(nameof(UnitColor), typeof(Color), typeof(ClamGaugeView), Colors.DimGray, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty UnitPositionProperty = BindableProperty.Create(nameof(UnitPosition), typeof(GaugeUnitPosition), typeof(ClamGaugeView), GaugeUnitPosition.Bottom, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ValueOffsetYProperty = BindableProperty.Create(nameof(ValueOffsetY), typeof(double), typeof(ClamGaugeView), 0.3d, validateValue: static (_, value) => ValidateFinite(value, nameof(ValueOffsetY)), propertyChanged: OnRenderInputChanged);

    public bool ValueVisible
    {
        get => (bool)GetValue(ValueVisibleProperty);
        set => SetValue(ValueVisibleProperty, value);
    }

    public string ValueFormat
    {
        get => (string)GetValue(ValueFormatProperty);
        set => SetValue(ValueFormatProperty, value);
    }

    public double ValueFontSize
    {
        get => (double)GetValue(ValueFontSizeProperty);
        set => SetValue(ValueFontSizeProperty, value);
    }

    public Color ValueColor
    {
        get => (Color)GetValue(ValueColorProperty);
        set => SetValue(ValueColorProperty, value);
    }

    public string? Unit
    {
        get => (string?)GetValue(UnitProperty);
        set => SetValue(UnitProperty, value);
    }

    public double UnitFontSize
    {
        get => (double)GetValue(UnitFontSizeProperty);
        set => SetValue(UnitFontSizeProperty, value);
    }

    public Color UnitColor
    {
        get => (Color)GetValue(UnitColorProperty);
        set => SetValue(UnitColorProperty, value);
    }

    public GaugeUnitPosition UnitPosition
    {
        get => (GaugeUnitPosition)GetValue(UnitPositionProperty);
        set => SetValue(UnitPositionProperty, value);
    }

    public double ValueOffsetY
    {
        get => (double)GetValue(ValueOffsetYProperty);
        set => SetValue(ValueOffsetYProperty, value);
    }
}
