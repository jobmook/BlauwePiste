// See https://aka.ms/new-console-template for more information
using Hangman;
using Hangman.DAL;
using System.Diagnostics;

Game currentGame;
ScoreTracker scoreTracker = new ScoreTracker();
Player player;

Console.WriteLine("\n++++++++++Welcome to Hangman++++++++++");

using (GameContext context = new GameContext())
{
    Console.WriteLine("Creating database...");
    context.Database.EnsureCreated();
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
        Console.WriteLine("Enter a secret word: ");
        string sw = Console.ReadLine();
        currentGame = new Game(sw.Trim().ToLower(), player.PlayerID); //  hoe kan visual studio weten dat player.PlayerID later gegenereerd wordt.
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
    while (!currentGame.Win() && !currentGame.Lose())
    {
        DisplayWordInfo();
        Console.WriteLine("Enter a guess letter: (press 1 to go back to the menu)");
        char guess = char.Parse(Console.ReadLine());
        if (guess == '1')
            break;
        currentGame.CheckGuess(guess);
    }

    Boolean gameFinish = false;
    if (currentGame.Win())
    {
        //player.PlayerGameWon();
        DisplayWordInfo();
        Console.WriteLine("You win!");
        stopWatch.Stop();
        currentGame.Time = stopWatch.ElapsedMilliseconds;
        scoreTracker.AddWonTime(currentGame.Time);
        gameFinish = true;
    }

    if (currentGame.Lose())
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
    //for (int i = 0; i < scoreTracker.WrongGuessCount.Count; i++)
    //{
    //    Console.WriteLine(scoreTracker.WrongGuessCount[i] + "______" + scoreTracker.WonTimes[i]);
    //}
    foreach(Game game in lastGames)
    {
        Console.WriteLine($"{game.WrongGuessedLetters.Length}______{game.Time}ms");
    }
}

void DisplayPlayerBoard()
{
    //List<Player> playerList = scoreTracker.Players;
    //Console.WriteLine("Players:");
    //foreach (Player p in playerList)
    //{
    //    Console.WriteLine($"Name: {p.Name}\nGames won: {p.GamesWon}, win ratio: {p.GetWinRatio()}\n");
    //}
}