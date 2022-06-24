using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class ScoreTracker
    {
        public int ScoreTrackerID { get; set; }
        public List<long> WonTimes { get; set; }
        public List<int> WrongGuessCount { get; set; }
        public List<Player> Players { get; set; }

        public ScoreTracker()
        {
            WonTimes = new List<long>(10);
            WrongGuessCount = new List<int>(10);
            Players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            if (player != null)
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
    }
}
