using CMS.Models;

namespace CMS.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player> GetPlayer(int id);
        Task<Player> CreatePlayer(Player player, IEnumerable<int> labelIds);
        Task<Player> UpdatePlayer(Player player, IEnumerable<int> labelIds);
        Task<Player> DeletePlayer(int id);
    }
}