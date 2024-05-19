using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using CMS.Services;
using CMS.Models.RequestModels;

namespace CMS.Controllers
{
    [ApiController]
    [Route("api/v1/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService service)
        {
            _playerService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _playerService.GetAllPlayers();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(int id)
        {
            var player = await _playerService.GetPlayer(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        // public async Task<IActionResult> Create(Player player, [FromBody] IEnumerable<int> labelIds)
        // {
        //     var createdPlayer = await _playerService.CreatePlayer(player, labelIds);
        //     return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.Id }, createdPlayer);
        // }
        public async Task<IActionResult> Create([FromBody] PlayerRequestModel playerRequestModel)
        {
            var createdPlayer = await _playerService.CreatePlayer(playerRequestModel.Player, playerRequestModel.LabelIds);
            return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.Id }, createdPlayer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PlayerRequestModel playerRequestModel)
        {
            if (id != playerRequestModel.Player.Id)
            {
                return BadRequest();
            }

            var existingPlayer = await _playerService.GetPlayer(id);
            if (existingPlayer == null)
            {
                return NotFound();
            }

            var updatedPlayer = await _playerService.UpdatePlayer(playerRequestModel.Player, playerRequestModel.LabelIds);
            return Ok(updatedPlayer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _playerService.DeletePlayer(id);
            return NoContent();
        }
    }
}