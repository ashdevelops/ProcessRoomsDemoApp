namespace ProcessRoomsDemoApp.Tasks;

public interface IServerTaskWorker : IDisposable
{
    Task WorkAsync(CancellationToken token);
}