namespace Example.Modules.Gauge;

public sealed class GaugeMenuViewModel : AppViewModelBase
{
    public IObserveCommand ForwardCommand { get; }

    public GaugeMenuViewModel()
    {
        ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
    }

    protected override Task OnNotifyBackAsync()
    {
        AndroidHelper.MoveTaskToBack();
        return Task.CompletedTask;
    }

    protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
}
