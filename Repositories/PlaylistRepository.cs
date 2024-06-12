using CMS.Data;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repositories
{
    public interface IPlaylistRepository : IBaseRepository<Playlist>
    {
        Task<List<Playlist>> GetPlaylistsByLabelNameAsync(string labelName);
    }

    public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(CmsDbContext context) : base(context)
        {
        }

        public override async Task<Playlist> GetByIdAsync(int id)
        {
            return await _context.Set<Playlist>()
                .Include(p => p.PlaylistLabels)
                    .ThenInclude(pl => pl.Label)
                .Include(p => p.PlaylistContentItems)
                    .ThenInclude(pci => pci.ContentItem)
                .Include(p => p.Schedule)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IEnumerable<Playlist>> GetAllAsync()
        {
            return await _context.Set<Playlist>()
                .Include(p => p.PlaylistLabels)
                    .ThenInclude(pl => pl.Label)
                .Include(p => p.PlaylistContentItems)
                    .ThenInclude(pci => pci.ContentItem)
                .Include(p => p.Schedule)
                .ToListAsync();
        }

        public async Task<List<Playlist>> GetPlaylistsByLabelNameAsync(string labelName)
        {
            return await _context.Set<Playlist>()
                .Include(p => p.PlaylistLabels)
                    .ThenInclude(pl => pl.Label)
                .Include(p => p.PlaylistContentItems)
                    .ThenInclude(pci => pci.ContentItem)
                .Include(p => p.Schedule)
                .Where(p => p.PlaylistLabels.Any(pl => pl.Label.Name == labelName))
                .ToListAsync();
        }
    }
}
