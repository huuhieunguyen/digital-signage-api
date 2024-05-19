using CMS.Models;

namespace CMS.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> GetPlayer(int id);
        Task<Player> CreatePlayer(Player player, IEnumerable<int> labelIds);
        Task<Player> UpdatePlayer(Player player, IEnumerable<int> labelIds);
        Task DeletePlayer(Player player);
    }
}