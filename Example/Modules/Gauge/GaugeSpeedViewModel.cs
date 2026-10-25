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

    [ObservableProperty]
    public partial Easing AnimationEasing { get; set; } = Easings[0];

    [ObservableProperty]
    public partial int AnimationDuration { get; set; } = Durations[0];

    [ObservableProperty]
    public partial GaugeUnitPosition UnitPosition { get; set; } = GaugeUnitPosition.Bottom;

    [ObservableProperty]
    public partial int EasingIndex { get; set; }

    [ObservableProperty]
    public partial int DurationIndex { get; set; }

    [ObservableProperty]
    public partial string UnitText { get; set; } = "単位 下";

    public IObserveCommand SelectEasingCommand { get; }

    public IObserveCommand SelectDurationCommand { get; }

    public IObserveCommand ToggleUnitCommand { get; }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeSpeedViewModel()
        : base(0, 200, 80, 0)
    {
        SelectEasingCommand = MakeDelegateCommand<int>(SelectEasing);
        SelectDurationCommand = MakeDelegateCommand<int>(SelectDuration);
        ToggleUnitCommand = MakeDelegateCommand(ToggleUnit);
    }

    //--------------------------------------------------------------------------------
    // Setting
    //--------------------------------------------------------------------------------

    private void SelectEasing(int index)
    {
        EasingIndex = index;
        AnimationEasing = Easings[index];
    }

    private void SelectDuration(int index)
    {
        DurationIndex = index;
        AnimationDuration = Durations[index];
    }

    private void ToggleUnit()
    {
        UnitPosition = UnitPosition == GaugeUnitPosition.Bottom ? GaugeUnitPosition.Right : GaugeUnitPosition.Bottom;
        UnitText = UnitPosition == GaugeUnitPosition.Bottom ? "単位 下" : "単位 右";
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------
}
