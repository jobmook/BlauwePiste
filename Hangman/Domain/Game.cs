using Hangman.Domain;
using System.Text.Json.Serialization;

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
        public int WordID { get; set; }

        public GameStatus Status { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }


        [JsonIgnore]
        public virtual Player Player { get; set; }
        [JsonIgnore]
        public virtual Word Word { get; set; }
    }
}