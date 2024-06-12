using Microsoft.AspNetCore.Mvc;
using CMS.Services;
using CMS.DTOs.PlayerDtos;

namespace CMS.Controllers
{
    [Route("api/v1/players")]
    [ApiController]
    public class PlayerController : BaseController<PlayerResponseDto, PlayerCreateRequestDto>
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService service) : base(service)
        {
            _playerService = service;
        }

        [HttpGet("filter-by-label")]
        public async Task<ActionResult<List<PlayerResponseDto>>> GetPlayersByLabelName([FromQuery] string labelName)
        {
            var result = await _playerService.GetPlayersByLabelNameAsync(labelName);
            return Ok(result);
        }

    }
}