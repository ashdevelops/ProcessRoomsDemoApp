namespace ProcessRoomsDemoApp.Tasks;

public interface IServerTask
{
    TimeSpan PeriodicInterval { get; }
    DateTime LastExecuted { get; set; }
    int LagTicks { get; set; }
    
    public bool WaitingToExecute()
    {
        return LastExecuted == default || DateTime.Now - LastExecuted >= PeriodicInterval;
    }

    Task ExecuteAsync();
    Task OnLagging(int ticks);
}