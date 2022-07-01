using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL.Repositories
{
    public class GamesRepository
    {
        public Game GetGameById(int id)
        {
            using (GameContext context = new GameContext())
            {
                return context.Games.Find(id);
            }
        }

        public void AddGame(Game gameToAdd)
        {
            using (GameContext context = new GameContext())
            {
                context.Games.AddAsync(gameToAdd);
                context.SaveChanges();
            }
        }

        public void UpdateGame(Game game)
        {
            using (GameContext context = new GameContext())
            {
               context.Games.Update(game);
               context.SaveChanges();
            }
        }

        public IEnumerable<Game> GetGames()
        {
            using (GameContext context = new GameContext())
            {
                return context.Games.ToList();
            }
        }

        public Game GetLastGame()
        {
            using (GameContext context = new GameContext())
            {
                return context.Games.OrderByDescending(x => x.GameID).FirstOrDefault();
            }
        }
    }
}
