namespace ClamGauge;

using System.Collections.ObjectModel;

using ClamGauge.Rendering;

using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

public partial class ClamGaugeView : SKCanvasView, IDisposable
{
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(ClamGaugeView), 0d, propertyChanged: OnValueChanged);
    public static readonly BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue), typeof(double), typeof(ClamGaugeView), 0d, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(ClamGaugeView), 100d, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty StartAngleProperty = BindableProperty.Create(nameof(StartAngle), typeof(double), typeof(ClamGaugeView), 225d, validateValue: static (_, value) => ValidateFinite(value, nameof(StartAngle)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty SweepAngleProperty = BindableProperty.Create(nameof(SweepAngle), typeof(double), typeof(ClamGaugeView), 270d, validateValue: static (_, value) => ValidateFinite(value, nameof(SweepAngle)), propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty RangesProperty = BindableProperty.Create(nameof(Ranges), typeof(IList<GaugeRange>), typeof(ClamGaugeView), defaultValueCreator: static _ => new ObservableCollection<GaugeRange>(), propertyChanged: OnRangesChanged);
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ClamGaugeView), "sans-serif", propertyChanged: OnFontFamilyChanged);

    private readonly GaugeCollectionWatcher rangesWatcher;
    private GaugeRenderer? renderer;
    private GaugeTextFormat? labelFormat;
    private GaugeTextFormat? valueFormat;
    private bool disposed;

#pragma warning disable CA1721
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
#pragma warning restore CA1721

    public double MinValue
    {
        get => (double)GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    public double MaxValue
    {
        get => (double)GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public double StartAngle
    {
        get => (double)GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    public double SweepAngle
    {
        get => (double)GetValue(SweepAngleProperty);
        set => SetValue(SweepAngleProperty, value);
    }

#pragma warning disable CA2227
    public IList<GaugeRange> Ranges
    {
        get => (IList<GaugeRange>)GetValue(RangesProperty);
        set => SetValue(RangesProperty, value);
    }
#pragma warning restore CA2227

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    public ClamGaugeView()
    {
        rangesWatcher = new GaugeCollectionWatcher(InvalidateSurface);
        rangesWatcher.Watch(Ranges);
    }

    //--------------------------------------------------------------------------------
    // Lifecycle
    //--------------------------------------------------------------------------------

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing || disposed)
        {
            return;
        }

        disposed = true;
        CompleteNeedleAnimation();
        rangesWatcher.Deactivate();
        renderer?.Dispose();
        renderer = null;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        rangesWatcher.Deactivate();
        if (args.NewHandler is null)
        {
            CompleteNeedleAnimation();
        }

        renderer?.Dispose();
        renderer = null;
        base.OnHandlerChanging(args);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        if (!disposed && (Handler is not null))
        {
            rangesWatcher.Activate();
        }

        InvalidateSurface();
    }

    //--------------------------------------------------------------------------------
    // Render
    //--------------------------------------------------------------------------------

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
        if (disposed || (Width <= 0) || (Height <= 0))
        {
            e.Surface.Canvas.Clear();
            return;
        }

        renderer ??= new GaugeRenderer(FontFamily);
        var canvas = e.Surface.Canvas;
        canvas.Save();
        try
        {
            canvas.Scale((float)(e.Info.Width / Width), (float)(e.Info.Height / Height));
            renderer.Render(canvas, (float)Width, (float)Height, CreateRenderState());
        }
        finally
        {
            canvas.Restore();
        }

        base.OnPaintSurface(e);
    }

    internal GaugeRenderState CreateRenderState() => new()
    {
        DisplayValue = DisplayValue,
        MinValue = MinValue,
        MaxValue = MaxValue,
        StartAngle = (float)StartAngle,
        SweepAngle = (float)SweepAngle,
        Ranges = Ranges,
        GaugeBackgroundStartColor = GaugeBackgroundStartColor,
        GaugeBackgroundEndColor = GaugeBackgroundEndColor,
        BezelOuterThickness = (float)BezelOuterThickness,
        BezelOuterStartColor = BezelOuterStartColor,
        BezelOuterEndColor = BezelOuterEndColor,
        BezelInnerThickness = (float)BezelInnerThickness,
        BezelInnerStartColor = BezelInnerStartColor,
        BezelInnerEndColor = BezelInnerEndColor,
        ArcInnerExtent = (float)ArcInnerExtent,
        ArcOuterExtent = (float)ArcOuterExtent,
        ArcTrackColor = ArcTrackColor,
        ArcStartColor = ArcStartColor,
        ArcEndColor = ArcEndColor,
        MajorTickInterval = MajorTickInterval,
        MajorTickInnerExtent = (float)MajorTickInnerExtent,
        MajorTickOuterExtent = (float)MajorTickOuterExtent,
        MajorTickThickness = (float)MajorTickThickness,
        MajorTickColor = MajorTickColor,
        MinorTickInterval = MinorTickInterval,
        MinorTickInnerExtent = (float)MinorTickInnerExtent,
        MinorTickOuterExtent = (float)MinorTickOuterExtent,
        MinorTickThickness = (float)MinorTickThickness,
        MinorTickColor = MinorTickColor,
        LabelInterval = LabelInterval,
        LabelExtent = (float)LabelExtent,
        LabelFormat = labelFormat ??= GaugeTextFormat.Parse(LabelFormat),
        LabelConverter = LabelConverter,
        LabelFormatter = LabelFormatter,
        LabelFontSize = (float)LabelFontSize,
        LabelColor = LabelColor,
        ValueVisible = ValueVisible,
        ValueFormat = valueFormat ??= GaugeTextFormat.Parse(ValueFormat),
        ValueFontSize = (float)ValueFontSize,
        ValueColor = ValueColor,
        Unit = Unit,
        UnitFontSize = (float)UnitFontSize,
        UnitColor = UnitColor,
        UnitPosition = UnitPosition,
        ValueOffsetY = (float)ValueOffsetY,
        NeedleEndExtent = (float)NeedleEndExtent,
        NeedleStartWidth = (float)NeedleStartWidth,
        NeedleEndWidth = (float)NeedleEndWidth,
        NeedleLeftColor = NeedleLeftColor,
        NeedleRightColor = NeedleRightColor,
        PivotRadius = (float)PivotRadius,
        PivotColor = PivotColor
    };

    private static void OnRenderInputChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((ClamGaugeView)bindable).InvalidateSurface();

    private static void OnRangesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (ClamGaugeView)bindable;
        view.rangesWatcher.Watch(newValue);
        view.InvalidateSurface();
    }

    private static void OnFontFamilyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (ClamGaugeView)bindable;
        view.renderer?.Dispose();
        view.renderer = null;
        view.InvalidateSurface();
    }

    private static void OnLabelFormatChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (ClamGaugeView)bindable;
        view.labelFormat = null;
        view.InvalidateSurface();
    }

    private static void OnValueFormatChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (ClamGaugeView)bindable;
        view.valueFormat = null;
        view.InvalidateSurface();
    }

    private static bool ValidateFinite(object? value, string name) =>
        ((value is double number) && Double.IsFinite(number)) ? true : throw new ArgumentOutOfRangeException(name, value, "The value must be a finite value.");

    private static bool ValidatePositive(object? value, string name) =>
        ((value is double number) && Double.IsFinite(number) && (number > 0)) ? true : throw new ArgumentOutOfRangeException(name, value, "The value must be a positive finite value.");

    private static bool ValidateNonNegative(object? value, string name) =>
        ((value is double number) && Double.IsFinite(number) && (number >= 0)) ? true : throw new ArgumentOutOfRangeException(name, value, "The value must be a finite value of zero or more.");

    private static bool ValidateDuration(object? value, string name) =>
        ((value is int number) && (number >= 0)) ? true : throw new ArgumentOutOfRangeException(name, value, "The value must be zero or more.");
}
