using System.Collections.Generic;
using CMS.Models;

namespace CMS.Repositories
{
    public interface IPlaylistRepository
    {
        IEnumerable<Playlist> GetAllPlaylists();
        Playlist GetPlaylist(int id);
        Playlist CreatePlaylist(Playlist playlist);
        Playlist UpdatePlaylist(Playlist playlist);
        Playlist DeletePlaylist(int id);
    }
}
