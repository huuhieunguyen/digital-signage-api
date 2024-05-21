using CMS.Data;
using CMS.Models;
using CMS.Services;
using Microsoft.EntityFrameworkCore;

namespace CMS.Services
{
    public class ScheduleBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ScheduleBackgroundService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1); // Adjust as needed

        public ScheduleBackgroundService(IServiceScopeFactory scopeFactory, ILogger<ScheduleBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckSchedulesAsync(stoppingToken);
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task CheckSchedulesAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CmsDbContext>();

                var currentTime = DateTime.UtcNow;
                var schedules = await context.Schedules
                    .Include(s => s.Player)
                    .Include(s => s.Playlist)
                    .Where(s => s.StartTime <= currentTime && s.EndTime >= currentTime)
                    .ToListAsync(stoppingToken);

                foreach (var schedule in schedules)
                {
                    // Logic to update player with current playlist
                    UpdatePlayerContent(schedule.Player, schedule.Playlist);
                }
            }
        }

        // This method is a placeholder where you should implement the logic to update the player. This could involve:
        // - Sending a message to the player via WebSocket or another real-time communication protocol.
        // - Updating a cache or database that the player periodically checks.
        // - Sending an HTTP request to the player if it exposes an endpoint for receiving updates.
        private void UpdatePlayerContent(Player player, Playlist playlist)
        {
            // Logic to update player content
            _logger.LogInformation($"Updating player {player.Name} with playlist {playlist.Title}");
            // This could involve sending an update to the player, caching the content, etc.
        }
    }

    // NOTE: Optimizing and Scaling Background Tasks
    //  Depending on the scale of your system, you might need to optimize and scale your background tasks:
    // - Interval Tuning: Adjust the `_checkInterval` to balance between responsiveness and resource usage.
    // - Concurrency: Handle potential concurrency issues if multiple background services or instances are running.
    // - Load Distribution: Distribute load efficiently if you have many players and schedules to check.

}
