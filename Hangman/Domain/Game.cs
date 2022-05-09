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
        public int GameID { get; set; }
        public string SecretWord { get; set; }
        public int Turns { get; set; }
        public int TriesLeft { get; set; }
        public string AllGuessedLetters { get; set; }
        public string CorrectGuessedLetters { get; set; }
        public string WrongGuessedLetters { get; set; }

        public int PlayerID { get; set; }

        public Boolean Won { get; set; }
        public long Time { get; set; }
      
        public virtual Player Player { get; set; }

        public Game(string secretWord, int playerID)
        {
            TriesLeft = 10; 
            Turns = 0;
            SecretWord = secretWord;
            AllGuessedLetters = "";
            CorrectGuessedLetters = "";
            WrongGuessedLetters = "";
            Won = false;
            PlayerID = playerID;
        }

        public void CheckGuess(char c)
        {
            Turns++;
            if (!AllGuessedLetters.Contains(c)) // .Count(x => x == c) methode
            {
                AllGuessedLetters += c;
                if (SecretWord.Contains(c))
                {
                    foreach (char occ in SecretWord)
                    {
                        if (occ == c)
                        {
                            CorrectGuessedLetters += (c);
                        }
                    }
                }
                else
                {
                    TriesLeft--;
                    WrongGuessedLetters += (c);
                }
            }   
        }

        public Boolean IsWon() 
        {
            if(CorrectGuessedLetters.Length == SecretWord.Length)
            {
                this.Won = true;
                return true;
            } else
            {
                return false;
            }
        }

        public Boolean IsLost() 
        {
            return TriesLeft <= 0;
        }

        public string GuessedLettersToString() //ui
        {
            string res = "";
            foreach(char c in AllGuessedLetters)
            {
                res += c;
                res += ", ";
            }
            return res;
        }

        public string UpdateSolutionString() //ui
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
    }
}