using Hangman;
using Hangman.DAL;
using Hangman.DAL.Repositories;
using Hangman.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HangmanWebApp.Pages.PlayGame
{
    
    public class PlayGameModel : PageModel
    {
        [BindProperty]
        public string PlayerName { get; set; }

        [BindProperty]
        public char Guess { get; set; }

        [BindProperty]
        public int TriesLeft { get; set; }

        [BindProperty]
        public int Turns { get; set; }

        [BindProperty]
        public string SolutionString { get; set; }

        [BindProperty]
        public string GuessedLettersString { get; set; }

        [BindProperty]
        public string GameProgress { get; set; }

        PlayersRepository players = new PlayersRepository();
        GamesRepository games = new GamesRepository();
        WordsRepository words = new WordsRepository();

        public void OnGet()
        {
            if (words.IsEmptyWordList() == true)
            {
                words.AddWords();
            }
        }

        public IActionResult OnGetNewGame()
        {
            return Page();
        }

        public IActionResult OnPostRegister()
        {
            Player newPlayer = new Player();
            newPlayer.Name = PlayerName;
            players.AddPlayer(newPlayer);
            return Page();
        }

        public void OnPostStart()
        {
            Game newGame = new Game();
            Word newWord = words.GetSecretWord();
            Player correspondingPlayer = players.GetLastPlayer();

            newGame.TriesLeft = 10;
            newGame.Turns = 0;
            newGame.SecretWord = newWord.SecretWord;
            newGame.AllGuessedLetters = "";
            newGame.CorrectGuessedLetters = "";
            newGame.WrongGuessedLetters = "";
            newGame.PlayerID = correspondingPlayer.PlayerID;
            newGame.WordID = newWord.WordID;
            newGame.Status = GameStatus.InProgress;

            games.AddGame(newGame);
            Turns = newGame.Turns;
            TriesLeft = newGame.TriesLeft;
            SolutionString = UpdateSolutionString(newGame);
            GuessedLettersString = GuessedLettersToString(newGame);
        }

        public IActionResult OnPostGuess()
        {
            Game currentGame = games.GetLastGame();
            Word currentWord = words.GetWordById(currentGame.WordID);
            GameProgress = "In Progress";

            currentGame.Turns++;
            if (!currentGame.AllGuessedLetters.Contains(this.Guess)) // .Count(x => x == c) methode
            {
                currentGame.AllGuessedLetters += this.Guess;
                if (currentGame.SecretWord.Contains(this.Guess))
                {
                    foreach (char occ in currentGame.SecretWord)
                    {
                        if (occ == this.Guess)
                        {
                            currentGame.CorrectGuessedLetters += (this.Guess);
                        }
                    }
                }
                else
                {
                    currentGame.TriesLeft--;
                    currentGame.WrongGuessedLetters += (this.Guess);
                    
                }

                if (IsWon(currentGame))
                {
                    currentGame.Status = GameStatus.Won;
                    GameProgress = "Won";
                    
                }

                if (IsLost(currentGame))
                {
                    currentGame.Status = GameStatus.Lost;
                    GameProgress = "Lost";
                    
                }
                games.UpdateGame(currentGame);
            }
            Turns = currentGame.Turns;
            TriesLeft = currentGame.TriesLeft;
            SolutionString = UpdateSolutionString(currentGame);
            GuessedLettersString = GuessedLettersToString(currentGame);
            return Page();
        }

        public string UpdateSolutionString(Game currentGame) //ui
        {
            string res = "";
            foreach (char l in currentGame.SecretWord)
            {
                if (currentGame.CorrectGuessedLetters.Contains(l))
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

        public string GuessedLettersToString(Game currentGame) //ui
        {
            string res = "";
            foreach (char c in currentGame.AllGuessedLetters)
            {
                res += c;
                res += ", ";
            }
            return res;
        }

        public Boolean IsWon(Game currentGame)
        {
            return currentGame.CorrectGuessedLetters.Length == currentGame.SecretWord.Length;
        }

        public Boolean IsLost(Game currentGame)
        {
            return currentGame.TriesLeft <= 0;
        }
    }
}