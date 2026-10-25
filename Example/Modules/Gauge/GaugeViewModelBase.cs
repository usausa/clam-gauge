namespace Example.Modules.Gauge;

using System.Security.Cryptography;

public abstract partial class GaugeViewModelBase : AppViewModelBase
{
    private readonly double minimum;
    private readonly double maximum;
    private readonly int digits;

    [ObservableProperty]
    public partial double Value { get; set; }

    [ObservableProperty]
    public partial bool IsSettingOpen { get; set; }

    public IObserveCommand RandomCommand { get; }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    protected GaugeViewModelBase(double minimum, double maximum, double value, int digits)
    {
        this.minimum = minimum;
        this.maximum = maximum;
        this.digits = digits;
        Value = value;
        RandomCommand = MakeDelegateCommand(MoveToRandomValue);
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    protected override Task OnNotifyBackAsync()
    {
        if (IsSettingOpen)
        {
            IsSettingOpen = false;
            return Task.CompletedTask;
        }

        return Navigator.ForwardAsync(ViewId.GaugeMenu);
    }

    protected override Task OnNotifyFunction1() => OnNotifyBackAsync();

    protected override Task OnNotifyFunction2()
    {
        IsSettingOpen = !IsSettingOpen;
        return Task.CompletedTask;
    }

    //--------------------------------------------------------------------------------
    // Operation
    //--------------------------------------------------------------------------------

    private void MoveToRandomValue()
    {
        var span = maximum - minimum;
        double next;
        do
        {
            next = Math.Round(minimum + (span * RandomNumberGenerator.GetInt32(0, 1001) / 1000), digits);
        }
        while (Math.Abs(next - Value) < span / 5);

        Value = next;
    }
}
