namespace ClamGauge;

public sealed partial class ClamGaugeView
{
    public static readonly BindableProperty ShowValueProperty = BindableProperty.Create(nameof(ShowValue), typeof(bool), typeof(ClamGaugeView), true, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ValueFormatProperty = BindableProperty.Create(nameof(ValueFormat), typeof(string), typeof(ClamGaugeView), "{0:N1}", propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ValueFontSizeProperty = BindableProperty.Create(nameof(ValueFontSize), typeof(float), typeof(ClamGaugeView), 28f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ValueColorProperty = BindableProperty.Create(nameof(ValueColor), typeof(Color), typeof(ClamGaugeView), Colors.Black, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty UnitProperty = BindableProperty.Create(nameof(Unit), typeof(string), typeof(ClamGaugeView), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty UnitFontSizeProperty = BindableProperty.Create(nameof(UnitFontSize), typeof(float), typeof(ClamGaugeView), 12f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty UnitColorProperty = BindableProperty.Create(nameof(UnitColor), typeof(Color), typeof(ClamGaugeView), Colors.DimGray, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty UnitPositionProperty = BindableProperty.Create(nameof(UnitPosition), typeof(GaugeUnitPosition), typeof(ClamGaugeView), GaugeUnitPosition.Bottom, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ValueOffsetYProperty = BindableProperty.Create(nameof(ValueOffsetY), typeof(float), typeof(ClamGaugeView), 0.3f, propertyChanged: OnRenderInputChanged);

    public bool ShowValue
    {
        get => (bool)GetValue(ShowValueProperty);
        set => SetValue(ShowValueProperty, value);
    }

    public string ValueFormat
    {
        get => (string)GetValue(ValueFormatProperty);
        set => SetValue(ValueFormatProperty, value);
    }

    public float ValueFontSize
    {
        get => (float)GetValue(ValueFontSizeProperty);
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

    public float UnitFontSize
    {
        get => (float)GetValue(UnitFontSizeProperty);
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

    public float ValueOffsetY
    {
        get => (float)GetValue(ValueOffsetYProperty);
        set => SetValue(ValueOffsetYProperty, value);
    }
}
