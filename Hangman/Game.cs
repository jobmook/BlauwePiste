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
        private string SecretWord { get; set; }
        private int Turns;
        private int TriesLeft;
        private List<char> AllGuessedLetters;
        private List<char> CorrectGuessedLetters;
        private List<char> WrongGuessedLetters;
        private Stopwatch StopWatch;
        private Boolean Won;

        public Game(string secretWord)
        {
            TriesLeft = 10; 
            Turns = 0;
            SecretWord = secretWord;
            AllGuessedLetters = new List<char>();
            CorrectGuessedLetters = new List<char>();
            WrongGuessedLetters = new List<char>();
            StopWatch = new Stopwatch();
            StopWatch.Start();
            Won = false;
        }

        public int GetTurns()
        {
            return Turns;
        }

        public int GetTriesLeft()
        {
            return TriesLeft;
        }

        public List<char> GetCorrectGuessedLetters()
        {
            return CorrectGuessedLetters;
        }

        public List<char> GetWrongGuessedLetters()
        {
            return WrongGuessedLetters;
        }

        public List<char> GetAllGuessedLetters()
        {
            return AllGuessedLetters;
        }

        public string GetSecretWord()
        {
            return SecretWord;
        }

        public void CheckGuess(char c)
        {
            Turns++;
            if (!AllGuessedLetters.Contains(c))
            {
                AllGuessedLetters.Add(c);
                if (SecretWord.Contains(c))
                {
                    foreach (char occ in SecretWord)
                    {
                        if (occ == c)
                        {
                            CorrectGuessedLetters.Add(c);
                        }
                    }
                }
                else
                {
                    TriesLeft--;
                    WrongGuessedLetters.Add(c);
                }
            }   
        }

        public Boolean Win()
        {
            if(CorrectGuessedLetters.Count == SecretWord.Length)
            {
                this.StopWatch.Stop();
                this.Won = true;
                return true;
            } else
            {
                return false;
            }
        }

        public Boolean Lose()
        {
            if(TriesLeft <= 0)
            {
                this.StopWatch.Stop();
                return true;
            } else
            {
                return false;
            }
        }

        public string GuessedLettersToString()
        {
            string res = "";
            foreach(char c in AllGuessedLetters)
            {
                res += c;
                res += ", ";
            }
            return res;
        }

        public string UpdateSolutionString()
        {
            string res = "";
            foreach (char l in SecretWord)
            {
                if (CorrectGuessedLetters.Contains(l))
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
            return Won;
        }

        public long GetTime()
        {
            return StopWatch.ElapsedMilliseconds;
        }
    }
}