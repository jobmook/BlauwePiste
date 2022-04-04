using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Game
    {
        private string _secretWord { get; set; }
        private int _turns;
        private int _triesLeft;
        private List<char> _allGuessedLetters;
        private List<char> _correctGuessedLetters;
        private List<char> _wrongGuessedLetters;
        private Stopwatch _stopWatch;
        private Boolean _won;

        public Game(string secretWord)
        {
            _triesLeft = 10; 
            _turns = 0;
            _secretWord = secretWord;
            _allGuessedLetters = new List<char>();
            _correctGuessedLetters = new List<char>();
            _wrongGuessedLetters = new List<char>();
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            _won = false;
        }

        public int GetTurns()
        {
            return _turns;
        }

        public int GetTriesLeft()
        {
            return _triesLeft;
        }

        public List<char> GetCorrectGuessedLetters()
        {
            return _correctGuessedLetters;
        }

        public List<char> GetWrongGuessedLetters()
        {
            return _wrongGuessedLetters;
        }

        public List<char> GetAllGuessedLetters()
        {
            return _allGuessedLetters;
        }

        public string GetSecretWord()
        {
            return _secretWord;
        }

        public void CheckGuess(char c)
        {
            _turns++;
            if (!_allGuessedLetters.Contains(c))
            {
                _allGuessedLetters.Add(c);
                if (_secretWord.Contains(c))
                {
                    foreach (char occ in _secretWord)
                    {
                        if (occ == c)
                        {
                            _correctGuessedLetters.Add(c);
                        }
                    }
                }
                else
                {
                    _triesLeft--;
                    _wrongGuessedLetters.Add(c);
                }
            }   
        }

        public Boolean Win()
        {
            if(_correctGuessedLetters.Count == _secretWord.Length)
            {
                this._stopWatch.Stop();
                this._won = true;
                return true;
            } else
            {
                return false;
            }
        }

        public Boolean Lose()
        {
            if(_triesLeft <= 0)
            {
                this._stopWatch.Stop();
                return true;
            } else
            {
                return false;
            }
        }

        public string GuessedLettersToString()
        {
            string res = "";
            foreach(char c in _allGuessedLetters)
            {
                res += c;
                res += ", ";
            }
            return res;
        }

        public string UpdateSolutionString()
        {
            string res = "";
            foreach (char l in _secretWord)
            {
                if (_correctGuessedLetters.Contains(l))
                {
                    res += l + " ";
                }
                else
                {
                    res += "* ";
                }
            }
            return res;
        }

        public Boolean GetWonStatus()
        {
            return _won;
        }

        public long GetTime()
        {
            return _stopWatch.ElapsedMilliseconds;
        }
    }
}