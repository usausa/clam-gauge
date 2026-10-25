namespace Example.Modules.Gauge;

public sealed partial class GaugeTemperatureViewModel : GaugeViewModelBase
{
    private static readonly (Color Start, Color End)[] Arcs =
    [
        (Color.FromArgb("#3080FF"), Color.FromArgb("#FF3010")),
        (Color.FromArgb("#E0F4FF"), Color.FromArgb("#2060E0")),
        (Color.FromArgb("#40C060"), Color.FromArgb("#E8E040")),
        (Color.FromArgb("#FFC040"), Color.FromArgb("#E02060")),
        (Color.FromArgb("#FFFFFF"), Color.FromArgb("#607090"))
    ];

    private static readonly GaugeRange[] Bands =
    [
        new() { StartValue = -10, EndValue = 10, StartColor = Color.FromArgb("#3080FF"), EndColor = Color.FromArgb("#40C060") },
        new() { StartValue = 10, EndValue = 25, StartColor = Color.FromArgb("#40C060"), EndColor = Color.FromArgb("#C8D040") },
        new() { StartValue = 25, EndValue = 35, StartColor = Color.FromArgb("#C8D040"), EndColor = Color.FromArgb("#FFA040") },
        new() { StartValue = 35, EndValue = 40, StartColor = Color.FromArgb("#FFA040"), EndColor = Color.FromArgb("#FF3010") }
    ];

    private static readonly double[] MinorTickIntervals = [1, 0.5, 0];

    [ObservableProperty]
    public partial IReadOnlyList<GaugeRange> Ranges { get; set; } = Bands;

    [ObservableProperty]
    public partial Color ArcStartColor { get; set; } = Arcs[0].Start;

    [ObservableProperty]
    public partial Color ArcEndColor { get; set; } = Arcs[0].End;

    [ObservableProperty]
    public partial double MinorTickInterval { get; set; } = MinorTickIntervals[0];

    [ObservableProperty]
    public partial double LabelInterval { get; set; } = 10;

    [ObservableProperty]
    public partial int MinorTickIndex { get; set; }

    [ObservableProperty]
    public partial string LabelText { get; set; } = "目盛り 10";

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

    public IObserveCommand SelectTickCommand { get; }

    public IObserveCommand ToggleLabelCommand { get; }

    public IReadOnlyList<ColorSwatch> ArcSwatches { get; }

    //--------------------------------------------------------------------------------
    // Constructor
    //--------------------------------------------------------------------------------

    public GaugeTemperatureViewModel()
        : base(-10, 40, 22, 1)
    {
        ArcSwatches = [.. Arcs.Select(x => ColorSwatch.Gradient(x.Start, x.End, MakeDelegateCommand(() => SelectArc(x.Start, x.End))))];
        BandsVisible = true;
        SelectTickCommand = MakeDelegateCommand<int>(SelectTick);
        ToggleLabelCommand = MakeDelegateCommand(ToggleLabel);
    }

    //--------------------------------------------------------------------------------
    // Setting
    //--------------------------------------------------------------------------------

    private void SelectTick(int index)
    {
        MinorTickIndex = index;
        MinorTickInterval = MinorTickIntervals[index];
    }

    private void ToggleLabel()
    {
        LabelInterval = LabelInterval > 5 ? 5 : 10;
        LabelText = $"目盛り {LabelInterval:N0}";
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------
    // Operation
    //--------------------------------------------------------------------------------

    private void SelectArc(Color start, Color end)
    {
        ArcStartColor = start;
        ArcEndColor = end;
        Ranges = [];
    }
}
