namespace Example.Modules.Gauge;

using Example.Converters;

public sealed partial class GaugeWindViewModel : GaugeViewModelBase
{
    private static readonly CompassLabelConverter Compass = new();

    private static readonly DegreeLabelConverter Degrees = new();

    private int labelMode;

    [ObservableProperty]
    public partial IValueConverter LabelConverter { get; set; } = Compass;

    [ObservableProperty]
    public partial double LabelInterval { get; set; } = 90;

    [ObservableProperty]
    public partial bool ShowValue { get; set; }

    [ObservableProperty]
    public partial int AnimationDuration { get; set; }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeWindViewModel()
        : base(0, 360, 90, 0)
    {
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    protected override Task OnNotifyFunction2()
    {
        labelMode = (labelMode + 1) % 3;
        LabelConverter = labelMode == 2 ? Degrees : Compass;
        LabelInterval = labelMode switch
        {
            0 => 90,
            1 => 45,
            _ => 30
        };
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction3()
    {
        AnimationDuration = AnimationDuration > 0 ? 0 : 500;
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction4()
    {
        ShowValue = !ShowValue;
        return Task.CompletedTask;
    }
}
