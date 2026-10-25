namespace Example.Modules.Gauge;

public sealed partial class GaugePressureViewModel : GaugeViewModelBase
{
    private static readonly (Color Start, Color End)[] Bands =
    [
        (Color.FromArgb("#FFB0B0"), Color.FromArgb("#CC0000")),
        (Color.FromArgb("#B0D0FF"), Color.FromArgb("#1060D0")),
        (Color.FromArgb("#B8E8B8"), Color.FromArgb("#209040")),
        (Color.FromArgb("#FFD8A0"), Color.FromArgb("#E07000")),
        (Color.FromArgb("#D8C0F0"), Color.FromArgb("#7030B0"))
    ];

    [ObservableProperty]
    public partial IReadOnlyList<GaugeRange> Ranges { get; set; } = CreateBand(Bands[0].Start, Bands[0].End);

    [ObservableProperty]
    public partial double LabelInterval { get; set; } = 20;

    [ObservableProperty]
    public partial string ValueFormat { get; set; } = "{0:N0}";

    [ObservableProperty]
    public partial GaugeUnitPosition UnitPosition { get; set; } = GaugeUnitPosition.Bottom;

    [ObservableProperty]
    public partial string UnitText { get; set; } = "単位 下";

    [ObservableProperty]
    public partial string LabelText { get; set; } = "目盛り 20";

    [ObservableProperty]
    public partial string ValueText { get; set; } = "値 小数なし";

    public IObserveCommand ToggleUnitCommand { get; }

    public IObserveCommand ToggleLabelCommand { get; }

    public IObserveCommand ToggleValueCommand { get; }

    public IReadOnlyList<ColorSwatch> BandSwatches { get; }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugePressureViewModel()
        : base(950, 1050, 1013, 0)
    {
        BandSwatches = [.. Bands.Select(x => ColorSwatch.Gradient(x.Start, x.End, MakeDelegateCommand(() => SelectBand(x.Start, x.End))))];
        ToggleUnitCommand = MakeDelegateCommand(ToggleUnit);
        ToggleLabelCommand = MakeDelegateCommand(ToggleLabel);
        ToggleValueCommand = MakeDelegateCommand(ToggleValue);
    }

    //--------------------------------------------------------------------------------
    // Setting
    //--------------------------------------------------------------------------------

    private void ToggleUnit()
    {
        UnitPosition = UnitPosition == GaugeUnitPosition.Bottom ? GaugeUnitPosition.Right : GaugeUnitPosition.Bottom;
        UnitText = UnitPosition == GaugeUnitPosition.Bottom ? "単位 下" : "単位 右";
    }

    private void ToggleLabel()
    {
        LabelInterval = LabelInterval > 10 ? 10 : 20;
        LabelText = $"目盛り {LabelInterval:N0}";
    }

    private void ToggleValue()
    {
        ValueFormat = ValueFormat == "{0:N0}" ? "{0:N1}" : "{0:N0}";
        ValueText = ValueFormat == "{0:N0}" ? "値 小数なし" : "値 小数 1 桁";
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------
    // Operation
    //--------------------------------------------------------------------------------

    private void SelectBand(Color start, Color end)
    {
        Ranges = CreateBand(start, end);
    }

    private static GaugeRange[] CreateBand(Color start, Color end) =>
        [new() { StartValue = 950, EndValue = 1050, StartColor = start, EndColor = end }];
}
