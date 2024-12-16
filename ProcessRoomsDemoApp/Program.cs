using ProcessRoomsDemoApp;
using ProcessRoomsDemoApp.Tasks;

var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
    .ConfigureServices((_, collection) => ServiceCollection.AddServices(collection))
    .Build();

host.Services.GetRequiredService<IServerTaskWorker>();