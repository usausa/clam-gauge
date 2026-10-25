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

    [ObservableProperty]
    public partial string UnitText { get; set; } = "単位 右";

    [ObservableProperty]
    public partial string LabelText { get; set; } = "目盛り 単位つき";

    public bool BandsVisible
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                RaisePropertyChanged(nameof(BandsVisible));
                Ranges = value ? Bands : [];
            }
        }
    }

    public IObserveCommand ToggleUnitCommand { get; }

    public IObserveCommand ToggleLabelCommand { get; }

    public IReadOnlyList<ColorSwatch> BackgroundSwatches { get; }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeHumidityViewModel()
        : base(0, 100, 50, 0)
    {
        BackgroundSwatches = [.. Backgrounds.Select(x => ColorSwatch.Solid(x, MakeDelegateCommand(() => SelectBackground(x))))];
        BandsVisible = true;
        ToggleUnitCommand = MakeDelegateCommand(ToggleUnit);
        ToggleLabelCommand = MakeDelegateCommand(ToggleLabel);
    }

    //--------------------------------------------------------------------------------
    // Setting
    //--------------------------------------------------------------------------------

    private void ToggleUnit()
    {
        UnitPosition = UnitPosition == GaugeUnitPosition.Right ? GaugeUnitPosition.Bottom : GaugeUnitPosition.Right;
        UnitText = UnitPosition == GaugeUnitPosition.Right ? "単位 右" : "単位 下";
    }

    private void ToggleLabel()
    {
        LabelFormat = LabelFormat == "{0:N0}%" ? "{0:N0}" : "{0:N0}%";
        LabelText = LabelFormat == "{0:N0}%" ? "目盛り 単位つき" : "目盛り 数値のみ";
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------
    // Operation
    //--------------------------------------------------------------------------------

    private void SelectBackground(Color color)
    {
        GaugeBackgroundStartColor = color;
    }
}
