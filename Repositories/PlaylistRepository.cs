using System.Linq;
using CMS.Data;
using CMS.Models;

namespace CMS.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly CmsDbContext _context;
        public PlaylistRepository(CmsDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Playlist> GetAllPlaylists()
        {
            return _context.Playlists.ToList();
        }

        public Playlist GetPlaylist(int id)
        {
            return _context.Playlists.FirstOrDefault(c => c.Id == id);
        }

        public Playlist CreatePlaylist(Playlist playlist)
        {
            _context.Playlists.Add(playlist);
            _context.SaveChanges();
            return playlist;
        }

        public Playlist UpdatePlaylist(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
            _context.SaveChanges();
            return playlist;
        }

        public Playlist DeletePlaylist(int id)
        {
            var playlist = _context.Playlists.Find(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                _context.SaveChanges();
                return playlist;
            }
            else
            {
                return null;
            }
        }
    }

}