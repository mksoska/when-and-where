using System.Timers;
using Microsoft.AspNetCore.Components;

namespace WhenAndWhere.Blazor.Components;

public partial class Countdown : ComponentBase, IDisposable
{
    private System.Timers.Timer _timer = null!;
    private int _secondsToRun = 0;

    protected string Time { get; set; } = "000 00:00:00";

    [Parameter]
    public EventCallback TimerOut { get; set; }

    public void Start(int secondsToRun)
    {
        _secondsToRun = secondsToRun;

        if (_secondsToRun > 0)
        {
            Time = TimeSpan.FromSeconds(_secondsToRun).ToString(@"ddd\ hh\:mm\:ss");
            StateHasChanged();
            _timer.Start();
        }
    }

    public void Stop()
    {
        _timer.Stop();
    }

    protected override void OnInitialized()
    {
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
    }

    private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
    {
        _secondsToRun--;

        await InvokeAsync(() =>
        {
            Time = TimeSpan.FromSeconds(_secondsToRun).ToString(@"ddd\ hh\:mm\:ss");
            StateHasChanged();
        });
        
        if (_secondsToRun <= 0)
        {
            _timer.Stop();
            await TimerOut.InvokeAsync();
        }
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}