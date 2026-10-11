namespace ClamGauge;

public sealed partial class ClamGaugeView
{
    public static readonly BindableProperty GaugeBackgroundColorProperty = BindableProperty.Create(nameof(GaugeBackgroundColor), typeof(Color), typeof(ClamGaugeView), Colors.Transparent, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty GaugeBackgroundColor2Property = BindableProperty.Create(nameof(GaugeBackgroundColor2), typeof(Color), typeof(ClamGaugeView), Colors.Transparent, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelOuterThicknessProperty = BindableProperty.Create(nameof(BezelOuterThickness), typeof(float), typeof(ClamGaugeView), 4f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelOuterStartColorProperty = BindableProperty.Create(nameof(BezelOuterStartColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#E8EEF8"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelOuterEndColorProperty = BindableProperty.Create(nameof(BezelOuterEndColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#6878A0"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelInnerThicknessProperty = BindableProperty.Create(nameof(BezelInnerThickness), typeof(float), typeof(ClamGaugeView), 2f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelInnerStartColorProperty = BindableProperty.Create(nameof(BezelInnerStartColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#4A5A88"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty BezelInnerEndColorProperty = BindableProperty.Create(nameof(BezelInnerEndColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#C0D0E8"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcInnerExtentProperty = BindableProperty.Create(nameof(ArcInnerExtent), typeof(float), typeof(ClamGaugeView), 0.77f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcOuterExtentProperty = BindableProperty.Create(nameof(ArcOuterExtent), typeof(float), typeof(ClamGaugeView), 0.87f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcTrackColorProperty = BindableProperty.Create(nameof(ArcTrackColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#20000000"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcStartColorProperty = BindableProperty.Create(nameof(ArcStartColor), typeof(Color), typeof(ClamGaugeView), Colors.DodgerBlue, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty ArcEndColorProperty = BindableProperty.Create(nameof(ArcEndColor), typeof(Color), typeof(ClamGaugeView), Colors.DodgerBlue, propertyChanged: OnRenderInputChanged);

    public Color GaugeBackgroundColor
    {
        get => (Color)GetValue(GaugeBackgroundColorProperty);
        set => SetValue(GaugeBackgroundColorProperty, value);
    }

    public Color GaugeBackgroundColor2
    {
        get => (Color)GetValue(GaugeBackgroundColor2Property);
        set => SetValue(GaugeBackgroundColor2Property, value);
    }

    public float BezelOuterThickness
    {
        get => (float)GetValue(BezelOuterThicknessProperty);
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

    public float BezelInnerThickness
    {
        get => (float)GetValue(BezelInnerThicknessProperty);
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

    public float ArcInnerExtent
    {
        get => (float)GetValue(ArcInnerExtentProperty);
        set => SetValue(ArcInnerExtentProperty, value);
    }

    public float ArcOuterExtent
    {
        get => (float)GetValue(ArcOuterExtentProperty);
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
