using AutoMapper;
using CMS.Data;
using CMS.DTOs.ScheduleDtos;
using CMS.Models;
using CMS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS.Services
{
    public interface IScheduleService : IBaseService<ScheduleResponseDto, ScheduleCreateRequestDto>
    {
    }

    public class ScheduleService : BaseService<Schedule, ScheduleResponseDto, ScheduleCreateRequestDto>, IScheduleService
    {
        public ScheduleService(IScheduleRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        // public async Task<Playlist> GetCurrentPlaylistForPlayer(int playerId)
        // {
        //     var now = DateTime.UtcNow;
        //     var schedule = await _context.Schedules
        //                                  .Include(s => s.Playlist)
        //                                  .FirstOrDefaultAsync(s => s.PlayerId == playerId && s.StartTime <= now && s.EndTime >= now);

        //     return schedule?.Playlist;
        // }
    }
}
