using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class ScoreTracker
    {
        private List<Game> _games;

        public ScoreTracker()
        {
            _games = new List<Game>();
        }

        public void AddGame(Game game)
        {
            if(game != null)
            {
                _games.Prepend(game);
            }
        }

        public int[,] StatisticsLast10()
        {
            int[,] wrongGuessed = new int[10,10];
            for (int i = 0; i < 10; i++)
            {
                if (_games[i] != null)
                {
                    wrongGuessed[i, 0] = _games[i].GetWrongGuessedLetters().Count;
                    if (_games[i].GetWonStatus() == true)
                        wrongGuessed[i, 1] = (int)_games[i].GetTime();
                    else
                        wrongGuessed[i, 1] = 0;
                } else
                {
                    wrongGuessed[i,0] = 0;
                    wrongGuessed[i,1] = 0;
                } 
            }
            return wrongGuessed;
        }
    }
}
