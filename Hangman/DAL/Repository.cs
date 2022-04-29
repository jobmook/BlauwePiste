using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL
{
    internal class Repository
    {
        public static IEnumerable<Game> AllGames()
        {
            using (GameContext context = new GameContext())
            {
                return context.Games.ToList();
            }
        }

        public static void AddGame(Game game)
        {
            using (GameContext context = new GameContext())
            {
                context.Games.Add(game);
                context.SaveChanges();
            }
        }

        public static IEnumerable<Player> AllPlayers()
        {
            using (GameContext context = new GameContext())
            {
                return context.Players.ToList();
            }
        }

        public static void AddPlayer(Player player)
        {
            using (GameContext context = new GameContext())
            {
                context.Players.Add(player);
                context.SaveChanges();
            }
        }

        public static List<Game> ReturnLast10Games()
        {
            using(GameContext context = new GameContext())
            {
                var stats = context.Games.OrderByDescending(k => k.GameID).Take(10).ToList();
                return stats;
            }
        }

        
    }
}
