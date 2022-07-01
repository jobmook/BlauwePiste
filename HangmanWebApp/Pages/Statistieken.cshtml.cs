using Hangman;
using Hangman.DAL;
using Hangman.DAL.Repositories;
using Hangman.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HangmanWebApp.Pages
{
    public class StatistiekenModel : PageModel
    {
        
        public IEnumerable<Game> Games { get; set; }

        StatsRepository repository = new StatsRepository();

        [BindProperty]
        public Game GameEntity { get; set; }

        [BindProperty]
        public int TimeDifference { get; set; }

        public void OnGet()
        {
            Games = repository.ReturnLast10Games();
        }
    }
}
