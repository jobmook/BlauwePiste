namespace HangmanWebApp.DTOs
{
    public class GuessDTO
    {
        public int GameId { get; set; }
        public char guessedLetter { get; set; }
    }
}
