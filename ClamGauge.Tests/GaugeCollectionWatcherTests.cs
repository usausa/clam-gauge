namespace ClamGauge.Tests;

public sealed class GaugeCollectionWatcherTests
{
    [Fact]
    public void ChangesAreReportedOnlyWhileActive()
    {
        // Arrange
        var count = 0;
        var watcher = new GaugeCollectionWatcher(() => count++);
        var ranges = new ObservableCollection<GaugeRange>();
        watcher.Watch(ranges);

        // Act
        ranges.Add(new GaugeRange());

        // Assert
        Assert.Equal(0, count);

        // Act
        watcher.Activate();
        ranges.Add(new GaugeRange());

        // Assert
        Assert.Equal(1, count);

        // Act
        watcher.Deactivate();
        ranges.Add(new GaugeRange());

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void ReplacedSourcesAreNoLongerWatched()
    {
        // Arrange
        var count = 0;
        var watcher = new GaugeCollectionWatcher(() => count++);
        var first = new ObservableCollection<GaugeRange>();
        var second = new ObservableCollection<GaugeRange>();
        watcher.Activate();
        watcher.Watch(first);

        // Act
        watcher.Watch(second);
        first.Add(new GaugeRange());
        second.Add(new GaugeRange());

        // Assert
        Assert.Equal(1, count);

        // Act
        watcher.Watch(new[] { new GaugeRange() });
        second.Add(new GaugeRange());

        // Assert
        Assert.Equal(1, count);
    }
}
