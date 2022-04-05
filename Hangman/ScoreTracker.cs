using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class ScoreTracker
    {
        private List<long> WonTimes;
        private List<int> WrongGuessCount;
        private List<Player> Players;

        public ScoreTracker()
        {
            WonTimes = new List<long>(10);
            WrongGuessCount = new List<int>(10);
            Players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            if(player != null)
            {
                Players.Add(player);  
            }
        }

        public void AddWonTime(long time)
        {
            WonTimes.Add(time);
        }

        public void AddWrongGuessCount(int wrongCount)
        {
            WrongGuessCount.Add(wrongCount);
        }

        public List<long> GetTimes()
        {
            return WonTimes;
        }

        public List<int> GetGuessCounts()
        {
            return WrongGuessCount;
        }

        public List<Player> GetPlayers()
        {
            return Players;
        }
        
    }
}
