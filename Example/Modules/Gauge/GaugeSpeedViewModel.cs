namespace Example.Modules.Gauge;

public sealed partial class GaugeSpeedViewModel : GaugeViewModelBase
{
    private static readonly Easing[] Easings =
    [
        Easing.CubicOut,
        Easing.Linear,
        Easing.SinInOut,
        Easing.BounceOut,
        Easing.SpringOut
    ];

    private static readonly int[] Durations = [1000, 300, 0];

    private int easingIndex;

    private int durationIndex;

    [ObservableProperty]
    public partial Easing AnimationEasing { get; set; } = Easings[0];

    [ObservableProperty]
    public partial int AnimationDuration { get; set; } = Durations[0];

    [ObservableProperty]
    public partial GaugeUnitPosition UnitPosition { get; set; } = GaugeUnitPosition.Bottom;

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeSpeedViewModel()
        : base(0, 200, 80, 0)
    {
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    protected override Task OnNotifyFunction2()
    {
        easingIndex = (easingIndex + 1) % Easings.Length;
        AnimationEasing = Easings[easingIndex];
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction3()
    {
        durationIndex = (durationIndex + 1) % Durations.Length;
        AnimationDuration = Durations[durationIndex];
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction4()
    {
        UnitPosition = UnitPosition == GaugeUnitPosition.Bottom ? GaugeUnitPosition.Right : GaugeUnitPosition.Bottom;
        return Task.CompletedTask;
    }
}
