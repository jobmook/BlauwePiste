using Hangman.Domain;

namespace Hangman
{
    public class Game
    {
        public int GameID { get; set; } // primary key
        public string SecretWord { get; set; }
        public int Turns { get; set; }
        public int TriesLeft { get; set; }
        public string AllGuessedLetters { get; set; }
        public string CorrectGuessedLetters { get; set; }
        public string WrongGuessedLetters { get; set; }

        public int PlayerID { get; set; }
        public int WordID { get; set; }

        public Boolean Won { get; set; }
        public long Time { get; set; }

        public virtual Player Player { get; set; }
        public virtual Word Word { get; set; }


        //public Game(string secretWord, int wordID, int playerID)
        //{
        //    TriesLeft = 10;
        //    Turns = 0;
        //    SecretWord = secretWord;
        //    AllGuessedLetters = "";
        //    CorrectGuessedLetters = "";
        //    WrongGuessedLetters = "";
        //    Won = false;
        //    PlayerID = playerID;
        //    WordID = wordID;
        //}

        //public void CheckGuess(char c)
        //{
        //    Turns++;
        //    if (!AllGuessedLetters.Contains(c)) // .Count(x => x == c) methode
        //    {
        //        AllGuessedLetters += c;
        //        if (SecretWord.Contains(c))
        //        {
        //            foreach (char occ in SecretWord)
        //            {
        //                if (occ == c)
        //                {
        //                    CorrectGuessedLetters += (c);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            TriesLeft--;
        //            WrongGuessedLetters += (c);
        //        }
        //    }
        //}

        //public Boolean IsWon()
        //{
        //    if (CorrectGuessedLetters.Length == SecretWord.Length)
        //    {
        //        this.Won = true;
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public Boolean IsLost()
        //{
        //    return TriesLeft <= 0;
        //}
    }
}