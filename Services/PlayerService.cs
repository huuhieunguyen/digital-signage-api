using CMS.Models;
using CMS.Repositories;

namespace CMS.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await _playerRepository.GetAllPlayers();
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await _playerRepository.GetPlayer(id);
        }

        public async Task<Player> CreatePlayer(Player player, IEnumerable<int> labelIds)
        {
            return await _playerRepository.CreatePlayer(player, labelIds);
        }

        public async Task<Player> UpdatePlayer(Player player, IEnumerable<int> labelIds)
        {
            return await _playerRepository.UpdatePlayer(player, labelIds);
        }

        public async Task<Player> DeletePlayer(int id)
        {
            var player = await _playerRepository.GetPlayer(id);
            if (player != null)
            {
                await _playerRepository.DeletePlayer(player);
            }
            return player;
        }
    }
}