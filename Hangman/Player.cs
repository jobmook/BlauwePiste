using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Player
    {
        private string Name;
        private int GamesWon;
        private int GamesLost;

        public Player(string name)
        {
            Name = name;
            GamesWon = 0;
            GamesLost = 0;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetGamesWon()
        {
            return GamesWon;
        }

        public int GetGamesLost()
        {
            return GamesLost;
        }

        public void PlayerGameWon()
        {
            GamesWon++;
        }

        public void PlayerGameLost()
        {
            GamesLost++;
        }

        public double GetWinRatio()
        {
            if(GamesLost == 0)
            {
                return GamesWon;
            } else
            {
                return (double) GamesWon / GamesLost;
            }
        }
    }
}