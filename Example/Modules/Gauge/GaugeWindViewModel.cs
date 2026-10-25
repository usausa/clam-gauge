namespace Example.Modules.Gauge;

using Example.Converters;

public sealed partial class GaugeWindViewModel : GaugeViewModelBase
{
    private static readonly CompassLabelConverter Compass = new();

    private static readonly DegreeLabelConverter Degrees = new();

    [ObservableProperty]
    public partial IValueConverter LabelConverter { get; set; } = Compass;

    [ObservableProperty]
    public partial double LabelInterval { get; set; } = 90;

    [ObservableProperty]
    public partial bool ValueVisible { get; set; }

    [ObservableProperty]
    public partial int AnimationDuration { get; set; }

    [ObservableProperty]
    public partial int LabelMode { get; set; }

    public bool NeedleAnimated
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                RaisePropertyChanged(nameof(NeedleAnimated));
                AnimationDuration = value ? 500 : 0;
            }
        }
    }

    public IObserveCommand SelectLabelModeCommand { get; }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeWindViewModel()
        : base(0, 360, 90, 0)
    {
        SelectLabelModeCommand = MakeDelegateCommand<int>(SelectLabelMode);
    }

    //--------------------------------------------------------------------------------
    // Setting
    //--------------------------------------------------------------------------------

    private void SelectLabelMode(int mode)
    {
        LabelMode = mode;
        LabelConverter = mode == 2 ? Degrees : Compass;
        LabelInterval = mode switch
        {
            0 => 90,
            1 => 45,
            _ => 30
        };
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------
}
