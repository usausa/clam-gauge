namespace Example.Modules.Gauge;

public sealed partial class GaugeRpmViewModel : GaugeViewModelBase
{
    [ObservableProperty]
    public partial bool ValueVisible { get; set; }

    [ObservableProperty]
    public partial double StartAngle { get; set; } = -150;

    [ObservableProperty]
    public partial double NeedleEndExtent { get; set; } = 0.94;

    [ObservableProperty]
    public partial double NeedleStartWidth { get; set; } = 8;

    [ObservableProperty]
    public partial double NeedleEndWidth { get; set; } = 2;

    [ObservableProperty]
    public partial Color NeedleLeftColor { get; set; } = Color.FromArgb("#CC4400");

    [ObservableProperty]
    public partial Color NeedleRightColor { get; set; } = Color.FromArgb("#DD5010");

    [ObservableProperty]
    public partial Color PivotColor { get; set; } = Color.FromArgb("#CC4400");

    public bool Symmetric
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                RaisePropertyChanged(nameof(Symmetric));
                StartAngle = value ? -135 : -150;
            }
        }
    }

    public bool WideNeedle
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                RaisePropertyChanged(nameof(WideNeedle));
                ApplyNeedle();
            }
        }
    }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeRpmViewModel()
        : base(0, 8, 3.5, 2)
    {
    }

    //--------------------------------------------------------------------------------
    // Setting
    //--------------------------------------------------------------------------------

    private void ApplyNeedle()
    {
        NeedleEndExtent = WideNeedle ? 0.72 : 0.94;
        NeedleStartWidth = WideNeedle ? 16 : 8;
        NeedleEndWidth = WideNeedle ? 6 : 2;
        NeedleLeftColor = Color.FromArgb(WideNeedle ? "#B0B0B8" : "#CC4400");
        NeedleRightColor = Color.FromArgb(WideNeedle ? "#D0D0D8" : "#DD5010");
        PivotColor = Color.FromArgb(WideNeedle ? "#B0B0B8" : "#CC4400");
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------
}
