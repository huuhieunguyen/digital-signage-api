using CMS.Models;

namespace CMS.Services
{
    public interface IPlaylistService
    {
        IEnumerable<Playlist> GetAllPlaylists();
        Playlist GetPlaylist(int id);
        Playlist CreatePlaylist(Playlist requestModel);
        Playlist UpdatePlaylist(int id, Playlist requestModel);
        Playlist DeletePlaylist(int id);
    }
}
