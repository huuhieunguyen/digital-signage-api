using CMS.DTOs;
using CMS.DTOs.PlaylistDtos;
using CMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/v1/playlists")]
    [ApiController]
    public class PlaylistController : BaseController<PlaylistResponseDto, PlaylistCreateRequestDto>
    {
        private readonly IPlaylistService _playlistService;
        public PlaylistController(IPlaylistService service) : base(service)
        {
            _playlistService = service;
        }

        [HttpGet("filter-by-label")]
        public async Task<ActionResult<List<PlaylistResponseDto>>> GetPlaylistsByLabelName([FromQuery] string labelName)
        {
            var result = await _playlistService.GetPlaylistsByLabelNameAsync(labelName);
            return Ok(result);
        }
    }
}
