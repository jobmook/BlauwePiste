// See https://aka.ms/new-console-template for more information
using Hangman;
using Hangman.DAL;
using Hangman.Domain;
using System.Diagnostics;

Game currentGame;
ScoreTracker scoreTracker = new ScoreTracker();
Player player;
Word word;

Console.WriteLine("\n++++++++++Welcome to Hangman++++++++++");

using (GameContext context = new GameContext())
{
    Console.WriteLine("Creating database...");
    context.Database.EnsureCreated();
    if (Repository.IsEmptyWordList())
    {
        GameInitialize.AddWords();
    }
    Console.WriteLine("Finished creating database!");
}

do
{
    Console.WriteLine("\nMENU");
    Console.WriteLine("Press 1 to play a Hangman game.");
    Console.WriteLine("Press 2 to to see the high scores.");
    Console.WriteLine("Press q to quit the program.");
    string input = Console.ReadLine();
    if (input.Equals("1"))
    {
        Console.WriteLine("Enter name:");
        string name = Console.ReadLine();
        player = new Player(name);
        Repository.AddPlayer(player);
        scoreTracker.AddPlayer(player);
        Console.WriteLine("Starting game..");
        //Console.WriteLine("Enter a secret word: "); // vervangen door stored procedure naar DB
        word = Repository.GetSecretWord();
        //string sw = Console.ReadLine();
        currentGame = new Game(word.SecretWord, word.WordID, player.PlayerID);
        gameController();
    }
    else if (input.Equals("2"))
    {
        Console.Clear();
        DisplayPlayerBoard();
    }
    else if (input == "q")
    {
        Console.WriteLine("Thanks for playing.");
        break;
    }
    else
    {
        Console.WriteLine("That is not a valid input.");
    }
} while (true);


void gameController()
{
    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();
    if (currentGame == null) throw new Exception("There is no game started!"); // error
    while (!currentGame.IsWon() && !currentGame.IsLost())
    {
        DisplayWordInfo();
        Console.WriteLine("Enter a guess letter: (press 1 to go back to the menu)");
        char guess = char.Parse(Console.ReadLine());
        if (guess == '1')
            break;
        currentGame.CheckGuess(guess);
    }

    Boolean gameFinish = false;
    if (currentGame.IsWon())
    {
        //player.PlayerGameWon();
        DisplayWordInfo();
        Console.WriteLine("You win!");
        stopWatch.Stop();
        currentGame.Time = stopWatch.ElapsedMilliseconds;
        scoreTracker.AddWonTime(currentGame.Time);
        gameFinish = true;
    }

    if (currentGame.IsLost())
    {
        //player.PlayerGameLost();
        DisplayWordInfo();
        Console.WriteLine("You lose!");
        stopWatch.Stop();
        currentGame.Time = stopWatch.ElapsedMilliseconds;
        scoreTracker.AddWonTime(-1);
        gameFinish = true;

    }

    if (gameFinish)
    {
        Console.WriteLine($"Correct word: {currentGame.SecretWord}");
        scoreTracker.AddWrongGuessCount(currentGame.WrongGuessedLetters.Length);
        Repository.AddGame(currentGame);
        DisplayScoreboard();
    }
}

void DisplayWordInfo()
{
    Console.WriteLine($"\nTurns left: {currentGame.TriesLeft}\nGuessed letters: {currentGame.GuessedLettersToString()}");
    Console.WriteLine(currentGame.UpdateSolutionString());
    Console.WriteLine();
}

void DisplayScoreboard()
{
    Console.WriteLine("\n++++++++++Statistics++++++++++");
    Console.WriteLine("Last 10 games:");
    Console.WriteLine("Wrong guessed ______ Time");
    List<Game> lastGames = Repository.ReturnLast10Games();
    foreach (Game game in lastGames)
    {
        Console.WriteLine($"{game.WrongGuessedLetters.Length}______{game.Time}ms");
    }
}

void DisplayPlayerBoard()
{
    var bestPlayer = Repository.ReturnBestMostGuessed();
    var bestRatio = Repository.ReturnBestRatio();
    Console.WriteLine($"Best player: id:{bestPlayer.PlayerID}, name: {bestPlayer.Name}, wins: {bestPlayer.WinCount}, losses: {bestPlayer.LostCount}, ratio: {bestPlayer.Ratio}");
    Console.WriteLine($"Best ratio: id:{bestRatio.PlayerID}, name: {bestRatio.Name}, wins: {bestRatio.WinCount}, losses: {bestRatio.LostCount}, ratio: {bestRatio.Ratio}");
}