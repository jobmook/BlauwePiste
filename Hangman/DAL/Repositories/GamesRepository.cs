using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL.Repositories
{
    public class GamesRepository
    {
        public async Task<Game> GetGameByIdAsync(int id)
        {
            using (GameContext context = new GameContext())
            {
                return await context.Games.FindAsync(id);
            }
        }

        public async Task AddGameAsync(Game gameToAdd)
        {
            using (GameContext context = new GameContext())
            {
                context.Games.Add(gameToAdd);
                context.SaveChangesAsync();
            }
        }

        public async Task UpdateGameAsync(Game game)
        {
            using (GameContext context = new GameContext())
            {
               context.Games.Update(game);
               await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            using (GameContext context = new GameContext())
            {
                return await context.Games.ToListAsync();
            }
        }

        public async Task<Game> GetLastGameAsync()
        {
            using (GameContext context = new GameContext())
            {
                return await context.Games.OrderByDescending(x => x.GameID).FirstOrDefaultAsync();
            }
        }
    }
}
