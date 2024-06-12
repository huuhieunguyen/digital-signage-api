using CMS.Models;
using CMS.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repositories
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Task<List<Playlist>> GetPlaylistsByPlayerLabelsAsync(int playerId);
    }

    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(CmsDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.PlayerLabels)
                .ThenInclude(pl => pl.Label)
                .ToListAsync();
        }

        public override async Task<Player> GetByIdAsync(int id)
        {
            return await _context.Set<Player>()
                .Include(p => p.PlayerLabels)
                .ThenInclude(pl => pl.Label)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Playlist>> GetPlaylistsByPlayerLabelsAsync(int playerId)
        {
            var playerLabels = await _context.Set<PlayerLabel>()
                .Where(pl => pl.PlayerId == playerId)
                .Select(pl => pl.LabelId)
                .ToListAsync();

            return await _context.Set<Playlist>()
                .Where(pl => pl.PlaylistLabels.Any(l => playerLabels.Contains(l.LabelId)))
                .Include(pl => pl.PlaylistContentItems)
                .ThenInclude(pci => pci.ContentItem)
                .ToListAsync();
        }

        public override async Task<Player> CreateAsync(Player entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return await _context.Set<Player>()
                .Include(p => p.PlayerLabels)
                .ThenInclude(pl => pl.Label)
                .FirstOrDefaultAsync(p => p.Id == entity.Id);
        }
    }
}

