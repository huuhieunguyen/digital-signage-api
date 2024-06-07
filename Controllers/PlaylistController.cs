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
        public PlaylistController(IPlaylistService service) : base(service)
        {
        }
    }
}
