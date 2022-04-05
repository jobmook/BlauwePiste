using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class ScoreTracker
    {
        private List<long> _wonTimes;
        private List<int> _wrongGuessCount;

        public ScoreTracker()
        {
            _wonTimes = new List<long>(10);
            _wrongGuessCount = new List<int>(10);
        }

        public void AddWonTime(long time)
        {
            _wonTimes.Prepend(time);
        }

        public void AddWrongGuessCount(int wrongCount)
        {
            _wrongGuessCount.Prepend(wrongCount);
        }

        public List<long> GetTimes()
        {
            return _wonTimes;
        }

        public List<int> GetGuessCount()
        {
            return _wrongGuessCount;
        }
        
    }
}
