using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL.Repositories
{
    public class PlayersRepository
    {
        
        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            using (GameContext context = new GameContext())
            {
                return await context.Players.FindAsync(id);
            }
        }

        public async Task<Player> GetPlayerByNameAsync(string name)
        {
            using (GameContext context = new GameContext())
            {
                return await context.Players.FirstOrDefaultAsync(x => x.Name == name);
            }
        }

        public async Task AddPlayerAsync(Player playerToAdd)
        {
            using (GameContext context = new GameContext())
            {
                context.Players.Add(playerToAdd);
                await context.SaveChangesAsync();
            }
        }

        public Task<Player> CreatePlayer(string npName)
        {
            Player newPlayer = new Player();
            newPlayer.Name = npName;

            using (GameContext context = new GameContext())
            {
                context.Players.Add(newPlayer);
                context.SaveChanges();
            }
            return Task.FromResult(newPlayer);
        }

        public async Task<IEnumerable<Player>> GetPlayersAsync()
        {
            using (GameContext context = new GameContext())
            {
                return await context.Players.ToListAsync();
            }
        }

        public async Task<Player> GetLastPlayerAsync()
        {
            using (GameContext context = new GameContext())
            {
                return await context.Players.OrderByDescending(x => x.PlayerID).FirstOrDefaultAsync();
            }
        }

    }
}
