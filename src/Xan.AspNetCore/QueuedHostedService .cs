using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xan.Extensions.Tasks;

namespace Xan.AspNetCore;

public sealed class QueuedHostedService(IBackgroundTaskQueue taskQueue, ILogger<QueuedHostedService> logger)
        : BackgroundService
{
    public IBackgroundTaskQueue TaskQueue { get; } = taskQueue ?? throw new ArgumentNullException(nameof(taskQueue));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Queued Hosted Service is running.");

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Func<CancellationToken, Task>? workItem = await TaskQueue.DequeueAsync(stoppingToken);
            if (workItem == null)
            {
                continue;
            }

            try
            {
                await workItem(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Error occurred executing {WorkItem}.", nameof(workItem));
            }
        }
    }

    public async override Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Queued Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }
}
