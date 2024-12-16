namespace ProcessRoomsDemoApp.API;

public interface IRoomRepository
{
    IRoomLogic? TryGetRoomById(int id);
    void AddRoom(IRoomLogic roomLogic);
    int Count { get; }
    IEnumerable<IRoomLogic> GetAllRooms();
    bool TryRemove(int id, out IRoomLogic? roomLogic);
    ValueTask DisposeAsync();
}