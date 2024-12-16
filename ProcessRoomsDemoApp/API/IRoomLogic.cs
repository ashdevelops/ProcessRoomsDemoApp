namespace ProcessRoomsDemoApp.API;

public interface IRoomLogic : IAsyncDisposable
{
    IRoomUserRepository UserRepository { get; }
    IRoomBotRepository BotRepository { get; }
}