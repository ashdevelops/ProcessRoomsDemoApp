namespace ProcessRoomsDemoApp.API;

public interface IRoomUser
{
    ValueTask RunPeriodicCheckAsync();
}