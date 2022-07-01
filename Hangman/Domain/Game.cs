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

        //public Boolean Won { get; set; }

        public GameStatus Status { get; set; }
        public long Time { get; set; }

        public virtual Player Player { get; set; }
        public virtual Word Word { get; set; }
    }
}