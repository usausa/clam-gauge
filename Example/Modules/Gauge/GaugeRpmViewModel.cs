namespace Example.Modules.Gauge;

public sealed partial class GaugeRpmViewModel : GaugeViewModelBase
{
    private bool symmetric;

    private bool wideNeedle;

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

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeRpmViewModel()
        : base(0, 8, 3.5, 2)
    {
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    protected override Task OnNotifyFunction2()
    {
        ValueVisible = !ValueVisible;
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction3()
    {
        symmetric = !symmetric;
        StartAngle = symmetric ? -135 : -150;
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction4()
    {
        wideNeedle = !wideNeedle;
        NeedleEndExtent = wideNeedle ? 0.72 : 0.94;
        NeedleStartWidth = wideNeedle ? 16 : 8;
        NeedleEndWidth = wideNeedle ? 6 : 2;
        NeedleLeftColor = Color.FromArgb(wideNeedle ? "#B0B0B8" : "#CC4400");
        NeedleRightColor = Color.FromArgb(wideNeedle ? "#F0F0F8" : "#DD5010");
        PivotColor = Color.FromArgb(wideNeedle ? "#707078" : "#CC4400");
        return Task.CompletedTask;
    }
}
