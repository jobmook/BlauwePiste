using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL.Repositories
{
    public class PlayersRepository
    {
        
        public Player GetPlayerById(int id)
        {
            using (GameContext context = new GameContext())
            {
                return context.Players.Find(id);
            }
        }

        public Player GetPlayerByName(string name)
        {
            using (GameContext context = new GameContext())
            {
                return context.Players.FirstOrDefault(x => x.Name == name);
            }
        }

        public void AddPlayer(Player playerToAdd)
        {
            using (GameContext context = new GameContext())
            {
                context.Players.Add(playerToAdd);
                context.SaveChanges();
            }
        }

        public IEnumerable<Player> GetPlayers()
        {
            using (GameContext context = new GameContext())
            {
                return context.Players.ToList();
            }
        }

        public Player GetLastPlayer()
        {
            using (GameContext context = new GameContext())
            {
                return context.Players.OrderByDescending(x => x.PlayerID).FirstOrDefault();
            }
        }

    }
}
