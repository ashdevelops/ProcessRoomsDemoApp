using System.Collections.Concurrent;

namespace ProcessRoomsDemoApp.API;

public class RoomUserRepository : IRoomUserRepository
{
    private readonly ConcurrentDictionary<long, IRoomUser> _users = new();

    public ICollection<IRoomUser> GetAll() => _users.Values;

    public bool TryAdd(IRoomUser user)
    {
        throw new NotImplementedException();
    }

    public bool TryGetById(long id, out IRoomUser? user)
    {
        throw new NotImplementedException();
    }

    public bool TryGetByUsername(string username, out IRoomUser? user)
    {
        throw new NotImplementedException();
    }

    public Task TryRemoveAsync(long id, bool notifyLeft, bool hotelView = false)
    {
        throw new NotImplementedException();
    }

    public int Count { get; }
    
    public async Task BroadcastDataAsync(AbstractPacketWriter writer, List<long>? excludedIds = null)
    {
        var serializedObject = NetworkPacketWriterSerializer.Serialize(writer);
        
        foreach (var roomUser in _users
                     .Values
                     .Where(x => excludedIds == null || !excludedIds.Contains(x.Id)))
        {
            await roomUser.NetworkObject.WriteToStreamAsync(serializedObject);
        }
    }

    public ICollection<IRoomUser> GetAllWithRights()
    {
        throw new NotImplementedException();
    }

    public async Task RunPeriodicCheckAsync()
    {
        try
        {
            await Parallel.ForEachAsync(_users.Values, async (user, _) =>
            {
                await user.RunPeriodicCheckAsync();
            });

            var users = _users
                .Values;

            if (!_users.IsEmpty)
            {
                var bots = users
                    .First()
                    .Room
                    .BotRepository
                    .GetAll();

                if (bots.Count != 0)
                {
                    await BroadcastDataAsync(new RoomBotStatusWriter
                    {
                        Bots = bots
                    });

                    await BroadcastDataAsync(new RoomBotDataWriter
                    {
                        Bots = bots
                    });
                }
            }
            
            var statusWriter = new RoomUserStatusWriter
            {
                Users = users
                    .Where(x => x.NeedsStatusUpdate)
                    .ToList()
            };

            var dataWriter = new RoomUserDataWriter
            {
                Users = users
            };

            await BroadcastDataAsync(statusWriter);
            await BroadcastDataAsync(dataWriter);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }
    
    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }
}