namespace ProcessRoomsDemoApp.API;

public interface IRoomBot
{
    ValueTask RunPeriodicCheckAsync();
}