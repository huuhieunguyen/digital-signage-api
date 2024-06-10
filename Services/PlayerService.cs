using AutoMapper;
using CMS.DTOs.PlayerDtos;
using CMS.Models;
using CMS.Repositories;

namespace CMS.Services
{
    public interface IPlayerService : IBaseService<PlayerResponseDto, PlayerCreateRequestDto>
    {
    }

    public class PlayerService : BaseService<Player, PlayerResponseDto, PlayerCreateRequestDto>, IPlayerService
    {
        public PlayerService(IPlayerRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }

}