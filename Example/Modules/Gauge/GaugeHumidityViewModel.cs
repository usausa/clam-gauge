namespace Example.Modules.Gauge;

public sealed partial class GaugeHumidityViewModel : GaugeViewModelBase
{
    private static readonly Color[] Backgrounds =
    [
        Color.FromArgb("#1A1E30"),
        Color.FromArgb("#103838"),
        Color.FromArgb("#381A38"),
        Color.FromArgb("#16301C"),
        Color.FromArgb("#2E2E2E")
    ];

    private static readonly GaugeRange[] Bands =
    [
        new() { StartValue = 0, EndValue = 40, StartColor = Color.FromArgb("#2060CC"), EndColor = Color.FromArgb("#2060CC") },
        new() { StartValue = 40, EndValue = 70, StartColor = Color.FromArgb("#2880FF"), EndColor = Color.FromArgb("#40A0FF") },
        new() { StartValue = 70, EndValue = 100, StartColor = Color.FromArgb("#40A0FF"), EndColor = Color.FromArgb("#60C0FF") }
    ];

    [ObservableProperty]
    public partial Color GaugeBackgroundStartColor { get; set; } = Backgrounds[0];

    [ObservableProperty]
    public partial IReadOnlyList<GaugeRange> Ranges { get; set; } = Bands;

    [ObservableProperty]
    public partial string LabelFormat { get; set; } = "{0:N0}%";

    [ObservableProperty]
    public partial GaugeUnitPosition UnitPosition { get; set; } = GaugeUnitPosition.Right;

    public IReadOnlyList<ColorSwatch> BackgroundSwatches { get; }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeHumidityViewModel()
        : base(0, 100, 50, 0)
    {
        BackgroundSwatches = [.. Backgrounds.Select(x => ColorSwatch.Solid(x, MakeDelegateCommand(() => SelectBackground(x))))];
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    protected override Task OnNotifyFunction2()
    {
        UnitPosition = UnitPosition == GaugeUnitPosition.Right ? GaugeUnitPosition.Bottom : GaugeUnitPosition.Right;
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction3()
    {
        LabelFormat = LabelFormat == "{0:N0}%" ? "{0:N0}" : "{0:N0}%";
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction4()
    {
        Ranges = Ranges.Count > 0 ? [] : Bands;
        return Task.CompletedTask;
    }

    //--------------------------------------------------------------------------------
    // Operation
    //--------------------------------------------------------------------------------

    private void SelectBackground(Color color)
    {
        GaugeBackgroundStartColor = color;
    }
}
