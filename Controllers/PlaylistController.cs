using Microsoft.AspNetCore.Mvc;
using CMS.Services;
using CMS.Models;
namespace CMS.Controllers
{
    [Route("api/v1/playlists")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpGet]
        public ActionResult<Playlist> GetAllPlaylists()
        {
            var playlist = _playlistService.GetAllPlaylists();
            return Ok(playlist);
        }

        [HttpGet("{id}", Name = "GetPlaylistById")]
        public ActionResult<Playlist> GetPlaylist(int id)
        {
            var playlist = _playlistService.GetPlaylist(id);
            if (playlist == null)
                return NotFound();

            return Ok(playlist);
        }

        [HttpPost]
        public ActionResult CreatePlaylist(Playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var newPlaylist = _playlistService.CreatePlaylist(playlist);
            return CreatedAtRoute("GetPlaylistById", new { id = newPlaylist.Id }, playlist);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePlaylist(int id, Playlist playlist)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPlaylist = _playlistService.UpdatePlaylist(id, playlist);
            if (updatedPlaylist == null)
                return NotFound();

            return Ok(updatedPlaylist);
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePlaylist(int id)
        {
            var deletedPlaylist = _playlistService.DeletePlaylist(id);
            if (deletedPlaylist == null)
                return NotFound();

            return Ok(deletedPlaylist);
        }
    }
}