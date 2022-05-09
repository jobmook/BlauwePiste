namespace Hangman.DAL
{
    public class PlayerStatistics
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public int WinCount { get; set; }
        public int LostCount { get; set; }
        public double Ratio { get; set; }

    }
}