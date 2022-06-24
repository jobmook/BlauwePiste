// See https://aka.ms/new-console-template for more information
using Hangman;
using Hangman.DAL;
using Hangman.Domain;
using System.Diagnostics;

Game currentGame = new Game();
ScoreTracker scoreTracker = new ScoreTracker();
Player player = new Player();
Word word = Repository.GetSecretWord();
GameController gameController = new GameController(player, currentGame, word);

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
        gameController.NewPlayer(name);
        Repository.AddPlayer(player);
        scoreTracker.AddPlayer(player);

        Console.WriteLine("Starting game..");
       
        gameController.NewGame(word.SecretWord, word.WordID, player.PlayerID);

        uiController();
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


void uiController()
{
    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();
    if (currentGame == null) throw new Exception("There is no game started!"); // error
    while (!gameController.IsWon() && !gameController.IsLost())
    {
        DisplayWordInfo();
        Console.WriteLine("Enter a guess letter: (press 1 to go back to the menu)");
        char guess = char.Parse(Console.ReadLine());
        if (guess == '1')
            break;
        gameController.CheckGuess(guess);
    }

    Boolean gameFinish = false;
    if (gameController.IsWon())
    {
        //player.PlayerGameWon();
        DisplayWordInfo();
        Console.WriteLine("You win!");
        stopWatch.Stop();
        currentGame.Time = stopWatch.ElapsedMilliseconds;
        scoreTracker.AddWonTime(currentGame.Time);
        gameFinish = true;
    }

    if (gameController.IsLost())
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
    var res = GuessedLettersToString();
    Console.WriteLine($"\nTurns left: {currentGame.TriesLeft}\nGuessed letters: {res}");
    Console.WriteLine(UpdateSolutionString());
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

string UpdateSolutionString() //ui
{
    string res = "";
    foreach (char l in word.SecretWord)
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


string GuessedLettersToString() //ui
{
    string res = "";
    foreach (char c in currentGame.AllGuessedLetters)
    {
        res += c;
        res += ", ";
    }
    return res;
}