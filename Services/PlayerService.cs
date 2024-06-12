using AutoMapper;
using CMS.DTOs.PlayerDtos;
using CMS.DTOs.PlaylistDtos;
using CMS.Models;
using CMS.Repositories;

namespace CMS.Services
{
    public interface IPlayerService : IBaseService<PlayerResponseDto, PlayerCreateRequestDto>
    {
        // Task<PlayerResponseDto> GetPlayerWithDetailsAsync(int id);
    }

    public class PlayerService : BaseService<Player, PlayerResponseDto, PlayerCreateRequestDto>, IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _playerRepository = repository;

        }

        public override async Task<PlayerResponseDto> GetByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return null;
            }

            var response = _mapper.Map<PlayerResponseDto>(player);

            var playlists = await _playerRepository.GetPlaylistsByPlayerLabelsAsync(id);
            response.Playlists = _mapper.Map<List<PlaylistSummaryDto>>(playlists);

            return response;
        }

        public override async Task<PlayerResponseDto> CreateAsync(PlayerCreateRequestDto request)
        {
            var player = _mapper.Map<Player>(request);
            var createdPlayer = await _playerRepository.CreateAsync(player);

            return await GetByIdAsync(createdPlayer.Id);
        }

        public override async Task<PlayerResponseDto> UpdateAsync(int id, PlayerCreateRequestDto request)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return null;
            }

            _mapper.Map(request, player);
            await _playerRepository.UpdateAsync(player);

            return await GetByIdAsync(id);
        }
    }

}