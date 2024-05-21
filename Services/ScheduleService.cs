using CMS.Data;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Services
{
    public class ScheduleService
    {
        private readonly CmsDbContext _context;

        public ScheduleService(CmsDbContext context)
        {
            _context = context;
        }

        public async Task<Playlist> GetCurrentPlaylistForPlayer(int playerId)
        {
            var now = DateTime.UtcNow;
            var schedule = await _context.Schedules
                                         .Include(s => s.Playlist)
                                         .FirstOrDefaultAsync(s => s.PlayerId == playerId && s.StartTime <= now && s.EndTime >= now);

            return schedule?.Playlist;
        }
    }

}