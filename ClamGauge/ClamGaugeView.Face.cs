namespace ClamGauge;

public partial class ClamGaugeView
{
    public static readonly BindableProperty GaugeBackgroundStartColorProperty = BindableProperty.Create(nameof(GaugeBackgroundStartColor), typeof(Color), typeof(ClamGaugeView), Colors.Transparent, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty GaugeBackgroundEndColorProperty = BindableProperty.Create(nameof(GaugeBackgroundEndColor), typeof(Color), typeof(ClamGaugeView), Colors.Transparent, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelOuterThicknessProperty = BindableProperty.Create(nameof(BezelOuterThickness), typeof(double), typeof(ClamGaugeView), 4d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(BezelOuterThickness)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelOuterStartColorProperty = BindableProperty.Create(nameof(BezelOuterStartColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#E8EEF8"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelOuterEndColorProperty = BindableProperty.Create(nameof(BezelOuterEndColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#6878A0"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelInnerThicknessProperty = BindableProperty.Create(nameof(BezelInnerThickness), typeof(double), typeof(ClamGaugeView), 2d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(BezelInnerThickness)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelInnerStartColorProperty = BindableProperty.Create(nameof(BezelInnerStartColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#4A5A88"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelInnerEndColorProperty = BindableProperty.Create(nameof(BezelInnerEndColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#C0D0E8"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcInnerExtentProperty = BindableProperty.Create(nameof(ArcInnerExtent), typeof(double), typeof(ClamGaugeView), 0.77d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(ArcInnerExtent)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcOuterExtentProperty = BindableProperty.Create(nameof(ArcOuterExtent), typeof(double), typeof(ClamGaugeView), 0.87d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(ArcOuterExtent)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcTrackColorProperty = BindableProperty.Create(nameof(ArcTrackColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#20000000"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcStartColorProperty = BindableProperty.Create(nameof(ArcStartColor), typeof(Color), typeof(ClamGaugeView), Colors.DodgerBlue, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcEndColorProperty = BindableProperty.Create(nameof(ArcEndColor), typeof(Color), typeof(ClamGaugeView), Colors.DodgerBlue, propertyChanged: OnRenderInputChanged);

    public Color GaugeBackgroundStartColor
    {
        get => (Color)GetValue(GaugeBackgroundStartColorProperty);
        set => SetValue(GaugeBackgroundStartColorProperty, value);
    }

    public Color GaugeBackgroundEndColor
    {
        get => (Color)GetValue(GaugeBackgroundEndColorProperty);
        set => SetValue(GaugeBackgroundEndColorProperty, value);
    }

    public double BezelOuterThickness
    {
        get => (double)GetValue(BezelOuterThicknessProperty);
        set => SetValue(BezelOuterThicknessProperty, value);
    }

    public Color BezelOuterStartColor
    {
        get => (Color)GetValue(BezelOuterStartColorProperty);
        set => SetValue(BezelOuterStartColorProperty, value);
    }

    public Color BezelOuterEndColor
    {
        get => (Color)GetValue(BezelOuterEndColorProperty);
        set => SetValue(BezelOuterEndColorProperty, value);
    }

    public double BezelInnerThickness
    {
        get => (double)GetValue(BezelInnerThicknessProperty);
        set => SetValue(BezelInnerThicknessProperty, value);
    }

    public Color BezelInnerStartColor
    {
        get => (Color)GetValue(BezelInnerStartColorProperty);
        set => SetValue(BezelInnerStartColorProperty, value);
    }

    public Color BezelInnerEndColor
    {
        get => (Color)GetValue(BezelInnerEndColorProperty);
        set => SetValue(BezelInnerEndColorProperty, value);
    }

    public double ArcInnerExtent
    {
        get => (double)GetValue(ArcInnerExtentProperty);
        set => SetValue(ArcInnerExtentProperty, value);
    }

    public double ArcOuterExtent
    {
        get => (double)GetValue(ArcOuterExtentProperty);
        set => SetValue(ArcOuterExtentProperty, value);
    }

    public Color ArcTrackColor
    {
        get => (Color)GetValue(ArcTrackColorProperty);
        set => SetValue(ArcTrackColorProperty, value);
    }

    public Color ArcStartColor
    {
        get => (Color)GetValue(ArcStartColorProperty);
        set => SetValue(ArcStartColorProperty, value);
    }

    public Color ArcEndColor
    {
        get => (Color)GetValue(ArcEndColorProperty);
        set => SetValue(ArcEndColorProperty, value);
    }
}
