using CMS.Repositories;
using CMS.Models;
namespace CMS.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        public PlaylistService(PlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public IEnumerable<Playlist> GetAllPlaylists()
        {
            return _playlistRepository.GetAllPlaylists();
        }

        public Playlist GetPlaylist(int id)
        {
            return _playlistRepository.GetPlaylist(id);
        }

        public Playlist CreatePlaylist(Playlist playlist)
        {
            // var playlist = new Playlist
            // {
            //     Title = contentItem.Title,
            //     Description = contentItem.Description,
            //     FilePath = contentItem.FilePath,
            //     ResourceType = contentItem.ResourceType,
            //     Duration = contentItem.Duration,
            //     Dimensions = contentItem.Dimensions,
            //     CreatedAt = DateTime.UtcNow,
            //     UpdatedAt = DateTime.UtcNow
            // };

            return _playlistRepository.CreatePlaylist(playlist);
        }

        public Playlist UpdatePlaylist(Playlist playlist)
        {
            return _playlistRepository.UpdatePlaylist(playlist);
        }

        public Playlist DeletePlaylist(int id)
        {
            return _playlistRepository.DeletePlaylist(id);
        }
    }

}