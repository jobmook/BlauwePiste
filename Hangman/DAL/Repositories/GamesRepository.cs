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
        private static List<Game> s_games = new();

        public async Task<Game> GetGameByIdAsync(int id)
        {
            Game foundgame;
            using (GameContext context = new GameContext())
            {
                foundgame =  await context.Games.FindAsync(id);
            }
            return foundgame;
        }

        public async Task AddGameAsync(Game gameToAdd)
        {
            using (GameContext context = new GameContext())
            {
                context.Games.Add(gameToAdd);
                await context.SaveChangesAsync();
            }
        }

        public Task<Game> CreateGameWithPlayer(Player player)
        {
            var game = new Game
            {
                Player = player,
                PlayerID = player.PlayerID,
        };


            using (GameContext context = new GameContext())
            {
                context.Games.Add(game);
                context.SaveChangesAsync();
            }

            return Task.FromResult(game);
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
