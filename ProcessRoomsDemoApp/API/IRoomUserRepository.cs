namespace ProcessRoomsDemoApp.API;

public interface IRoomUserRepository : IAsyncDisposable
{
    ICollection<IRoomUser> GetAll();
    bool TryAdd(IRoomUser user);
    bool TryGetById(long id, out IRoomUser? user);
    bool TryGetByUsername(string username, out IRoomUser? user);
    Task TryRemoveAsync(long id, bool notifyLeft, bool hotelView = false);
    int Count { get; }
    ICollection<IRoomUser> GetAllWithRights();
    Task RunPeriodicCheckAsync();
}