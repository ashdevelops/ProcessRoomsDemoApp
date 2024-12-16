using System.Diagnostics;

namespace ProcessRoomsDemoApp.Tasks;

public class ServerTaskWorker(
    IEnumerable<IServerTask> tasks) : IServerTaskWorker
{
    public async Task WorkAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var tasksWaiting = tasks
                .Where(x => x.WaitingToExecute())
                .ToList();
            
            await Parallel.ForEachAsync(tasksWaiting, token, async (t, _) =>
            {
                t.LastExecuted = DateTime.Now;
                await ProcessTaskAsync(t);
            });
            
            await Task.Delay(50, token);
        }
    }

    private static async Task ProcessTaskAsync(IServerTask task)
    {
        try
        {
            var stopwatch = Stopwatch.StartNew();
            await task.ExecuteAsync();
            stopwatch.Stop();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void Dispose()
    {
    }
}