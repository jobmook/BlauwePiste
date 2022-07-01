using Hangman.DAL;
using Hangman.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HangmanWebApp.Pages.HighScores
{
    public class HighScoresModel : PageModel
    {
        public PlayerStatistics BestMostGuessedPlayer { get; set; }
        public PlayerStatistics BestMostRatioPlayer { get; set; }

        StatsRepository repository = new StatsRepository();

        [BindProperty]
        public PlayerStatistics PlayerStatisticsEntity { get; set; }

        public void OnGet()
        {
            BestMostGuessedPlayer = repository.ReturnBestMostGuessed();
            BestMostRatioPlayer = repository.ReturnBestRatio();
        }
    }
}
