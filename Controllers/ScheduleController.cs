using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using CMS.Services;
using CMS.Models.RequestModels;
using CMS.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly CmsDbContext _context;

        public SchedulesController(CmsDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/current-playlist")]
        public async Task<ActionResult<Playlist>> GetCurrentPlaylist(int id)
        {
            var scheduleService = new ScheduleService(_context);
            var playlist = await scheduleService.GetCurrentPlaylistForPlayer(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            return await _context.Schedules
                                 .Include(s => s.Player)
                                 .Include(s => s.Playlist)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules
                                         .Include(s => s.Player)
                                         .Include(s => s.Playlist)
                                         .FirstOrDefaultAsync(s => s.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }

        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, schedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }

}