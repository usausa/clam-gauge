namespace ClamGauge;

public partial class ClamGaugeView
{
    public static readonly BindableProperty NeedleEndExtentProperty = BindableProperty.Create(nameof(NeedleEndExtent), typeof(double), typeof(ClamGaugeView), 0.75d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(NeedleEndExtent)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty NeedleStartWidthProperty = BindableProperty.Create(nameof(NeedleStartWidth), typeof(double), typeof(ClamGaugeView), 10d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(NeedleStartWidth)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty NeedleEndWidthProperty = BindableProperty.Create(nameof(NeedleEndWidth), typeof(double), typeof(ClamGaugeView), 2d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(NeedleEndWidth)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty NeedleLeftColorProperty = BindableProperty.Create(nameof(NeedleLeftColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#CC2828"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty NeedleRightColorProperty = BindableProperty.Create(nameof(NeedleRightColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#EE3C3C"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty PivotRadiusProperty = BindableProperty.Create(nameof(PivotRadius), typeof(double), typeof(ClamGaugeView), 8d, validateValue: static (_, value) => ValidateNonNegative(value, nameof(PivotRadius)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty PivotColorProperty = BindableProperty.Create(nameof(PivotColor), typeof(Color), typeof(ClamGaugeView), Color.FromArgb("#CC2828"), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty AnimationDurationProperty = BindableProperty.Create(nameof(AnimationDuration), typeof(int), typeof(ClamGaugeView), 300, validateValue: static (_, value) => ValidateDuration(value, nameof(AnimationDuration)));
    public static readonly BindableProperty AnimationEasingProperty = BindableProperty.Create(nameof(AnimationEasing), typeof(Easing), typeof(ClamGaugeView), Easing.CubicOut);

    private const string NeedleAnimationName = "ClamGaugeViewNeedle";

    private bool displayValueInitialized;

    public double NeedleEndExtent
    {
        get => (double)GetValue(NeedleEndExtentProperty);
        set => SetValue(NeedleEndExtentProperty, value);
    }

    public double NeedleStartWidth
    {
        get => (double)GetValue(NeedleStartWidthProperty);
        set => SetValue(NeedleStartWidthProperty, value);
    }

    public double NeedleEndWidth
    {
        get => (double)GetValue(NeedleEndWidthProperty);
        set => SetValue(NeedleEndWidthProperty, value);
    }

    public Color NeedleLeftColor
    {
        get => (Color)GetValue(NeedleLeftColorProperty);
        set => SetValue(NeedleLeftColorProperty, value);
    }

    public Color NeedleRightColor
    {
        get => (Color)GetValue(NeedleRightColorProperty);
        set => SetValue(NeedleRightColorProperty, value);
    }

    public double PivotRadius
    {
        get => (double)GetValue(PivotRadiusProperty);
        set => SetValue(PivotRadiusProperty, value);
    }

    public Color PivotColor
    {
        get => (Color)GetValue(PivotColorProperty);
        set => SetValue(PivotColorProperty, value);
    }

    public int AnimationDuration
    {
        get => (int)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    public Easing AnimationEasing
    {
        get => (Easing)GetValue(AnimationEasingProperty);
        set => SetValue(AnimationEasingProperty, value);
    }

    internal double DisplayValue { get; private set; }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((ClamGaugeView)bindable).MoveNeedle((double)newValue);

    private void MoveNeedle(double value)
    {
        if (disposed || !displayValueInitialized || (AnimationDuration <= 0) || (Handler is null))
        {
            displayValueInitialized = true;
            this.AbortAnimation(NeedleAnimationName);
            DisplayValue = value;
            InvalidateSurface();
            return;
        }

        this.Animate(NeedleAnimationName, OnNeedleFrame, DisplayValue, value, length: (uint)AnimationDuration, easing: AnimationEasing);
    }

    private void OnNeedleFrame(double value)
    {
        DisplayValue = value;
        InvalidateSurface();
    }

    private void CompleteNeedleAnimation()
    {
        this.AbortAnimation(NeedleAnimationName);
        DisplayValue = Value;
    }
}
