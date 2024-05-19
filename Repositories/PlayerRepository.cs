using CMS.Repositories;
using CMS.Models;
using CMS.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly CmsDbContext _dbContext;

        public PlayerRepository(CmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await _dbContext.Players
                .Include(p => p.PlayerLabels)
                .ThenInclude(pl => pl.Label)
                .ToListAsync();
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await _dbContext.Players
                .Include(p => p.PlayerLabels)
                .ThenInclude(pl => pl.Label)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Player> CreatePlayer(Player player, IEnumerable<int> labelIds)
        {
            player.PlayerLabels = labelIds.Select(labelId => new PlayerLabel
            {
                Player = player,
                LabelId = labelId
            }).ToList();

            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();
            return player;
        }

        public async Task<Player> UpdatePlayer(Player player, IEnumerable<int> labelIds)
        {
            var existingPlayer = await GetPlayer(player.Id);
            if (existingPlayer == null)
            {
                return null;
            }

            // Update player's labels
            existingPlayer.PlayerLabels = labelIds.Select(labelId => new PlayerLabel
            {
                PlayerId = player.Id,
                LabelId = labelId
            }).ToList();

            // Update other properties of the player
            existingPlayer.Name = player.Name;
            existingPlayer.Location = player.Location;
            existingPlayer.LastActiveDateTime = player.LastActiveDateTime;
            existingPlayer.IPAddress = player.IPAddress;
            existingPlayer.VirtualUrl = player.VirtualUrl;
            existingPlayer.Resolution = player.Resolution;
            existingPlayer.Orientation = player.Orientation;
            existingPlayer.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return existingPlayer;
        }

        // public async Task<Player> DeletePlayer(int id)
        // {
        //     var player = await GetPlayer(id);
        //     if (player == null)
        //     {
        //         return null;
        //     }

        //     _dbContext.Players.Remove(player);
        //     await _dbContext.SaveChangesAsync();
        //     return player;
        // }

        public async Task DeletePlayer(Player player)
        {
            _dbContext.Players.Remove(player);
            await _dbContext.SaveChangesAsync();
        }
    }
    // public class PlayerRepository : IPlayerRepository
    // {
    //     private readonly CmsDbContext _dbContext;
    //     public PlayerRepository(CmsDbContext dbContext)
    //     {
    //         _dbContext = dbContext;
    //     }

    //     public IEnumerable<Player> GetAllPlayers()
    //     {
    //         return _dbContext.Players;
    //     }

    //     public Player GetPlayer(int id)
    //     {
    //         return _dbContext.Players.Find(id);
    //     }

    //     public Player CreatePlayer(Player player)
    //     {
    //         _dbContext.Players.Add(player);
    //         _dbContext.SaveChanges();
    //         return player;
    //     }

    //     public Player UpdatePlayer(Player player)
    //     {
    //         _dbContext.Players.Update(player);
    //         _dbContext.SaveChanges();
    //         return player;
    //     }

    //     public Player DeletePlayer(int id)
    //     {
    //         var player = _dbContext.Players.Find(id);
    //         if (player == null)
    //         {
    //             return null;
    //         }

    //         _dbContext.Players.Remove(player);
    //         _dbContext.SaveChanges();
    //         return player;
    //     }
    // }
}