using CMS.Data;
using CMS.Models;

namespace CMS.Repositories
{
    public interface IPlaylistRepository : IBaseRepository<Playlist>
    {
    }

    public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(CmsDbContext context) : base(context)
        {
        }
    }
}
