using Microsoft.Extensions.DependencyInjection;
using ProcessRoomsDemoApp.Tasks;

namespace ProcessRoomsDemoApp;

public static class ServiceCollection
{
    public static void AddServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IServerTaskWorker, ServerTaskWorker>();
    }
}