namespace ClamGauge;

using System.Collections.ObjectModel;

using ClamGauge.Rendering;

using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

public sealed partial class ClamGaugeView : SKCanvasView
{
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(ClamGaugeView), 0d, propertyChanged: OnValueChanged);
    public static readonly BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue), typeof(double), typeof(ClamGaugeView), 0d, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(ClamGaugeView), 100d, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty StartAngleProperty = BindableProperty.Create(nameof(StartAngle), typeof(float), typeof(ClamGaugeView), 225f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty SweepAngleProperty = BindableProperty.Create(nameof(SweepAngle), typeof(float), typeof(ClamGaugeView), 270f, propertyChanged: OnRenderInputChanged);
    public static readonly BindableProperty RangesProperty = BindableProperty.Create(nameof(Ranges), typeof(IList<GaugeRange>), typeof(ClamGaugeView), defaultValueCreator: static _ => new ObservableCollection<GaugeRange>(), propertyChanged: OnRangesChanged);
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ClamGaugeView), "sans-serif", propertyChanged: OnFontFamilyChanged);

    private readonly GaugeCollectionWatcher rangesWatcher;
    private GaugeRenderer? renderer;

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

    public float StartAngle
    {
        get => (float)GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    public float SweepAngle
    {
        get => (float)GetValue(SweepAngleProperty);
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
        if (Handler is not null)
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
        if ((Width <= 0) || (Height <= 0))
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
        StartAngle = StartAngle,
        SweepAngle = SweepAngle,
        Ranges = Ranges,
        GaugeBackgroundColor = GaugeBackgroundColor,
        GaugeBackgroundColor2 = GaugeBackgroundColor2,
        BezelOuterThickness = BezelOuterThickness,
        BezelOuterStartColor = BezelOuterStartColor,
        BezelOuterEndColor = BezelOuterEndColor,
        BezelInnerThickness = BezelInnerThickness,
        BezelInnerStartColor = BezelInnerStartColor,
        BezelInnerEndColor = BezelInnerEndColor,
        ArcInnerExtent = ArcInnerExtent,
        ArcOuterExtent = ArcOuterExtent,
        ArcTrackColor = ArcTrackColor,
        ArcStartColor = ArcStartColor,
        ArcEndColor = ArcEndColor,
        MajorTickInterval = MajorTickInterval,
        MajorTickInnerExtent = MajorTickInnerExtent,
        MajorTickOuterExtent = MajorTickOuterExtent,
        MajorTickThickness = MajorTickThickness,
        MajorTickColor = MajorTickColor,
        MinorTickInterval = MinorTickInterval,
        MinorTickInnerExtent = MinorTickInnerExtent,
        MinorTickOuterExtent = MinorTickOuterExtent,
        MinorTickThickness = MinorTickThickness,
        MinorTickColor = MinorTickColor,
        LabelInterval = LabelInterval,
        LabelExtent = LabelExtent,
        LabelFormat = LabelFormat,
        LabelConverter = LabelConverter,
        LabelFormatter = LabelFormatter,
        LabelFontSize = LabelFontSize,
        LabelColor = LabelColor,
        ShowValue = ShowValue,
        ValueFormat = ValueFormat,
        ValueFontSize = ValueFontSize,
        ValueColor = ValueColor,
        Unit = Unit,
        UnitFontSize = UnitFontSize,
        UnitColor = UnitColor,
        UnitPosition = UnitPosition,
        ValueOffsetY = ValueOffsetY,
        NeedleEndExtent = NeedleEndExtent,
        NeedleStartWidth = NeedleStartWidth,
        NeedleEndWidth = NeedleEndWidth,
        NeedleLeftColor = NeedleLeftColor,
        NeedleRightColor = NeedleRightColor,
        PivotRadius = PivotRadius,
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
}
