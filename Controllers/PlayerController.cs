using Microsoft.AspNetCore.Mvc;
using CMS.Services;
using CMS.DTOs.PlayerDtos;

namespace CMS.Controllers
{
    [Route("api/v1/players")]
    [ApiController]
    public class PlayerController : BaseController<PlayerResponseDto, PlayerCreateRequestDto>
    {
        public PlayerController(IPlayerService service) : base(service)
        {
        }
    }
}