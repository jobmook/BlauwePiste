// See https://aka.ms/new-console-template for more information
using Hangman;
using System.Diagnostics;

Game currentGame;
ScoreTracker scoreTracker = new ScoreTracker();
Player player;

Console.WriteLine("\n++++++++++Welcome to Hangman++++++++++");
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
        scoreTracker.AddPlayer(player);
        Console.WriteLine("Starting game..");
        Console.WriteLine("Enter a secret word: ");
        string sw = Console.ReadLine();
        currentGame = new Game(sw.Trim().ToLower());
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
    
    Stopwatch sw = Stopwatch.StartNew();
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
        player.PlayerGameWon();
        DisplayWordInfo();
        Console.WriteLine("You win!");
        scoreTracker.AddWonTime(currentGame.GetTime());
        gameFinish = true;
    }

    if (currentGame.Lose())
    {
        player.PlayerGameLost();
        DisplayWordInfo();
        Console.WriteLine("You lose!");
        scoreTracker.AddWonTime(-1);
        gameFinish = true;

    }

    if (gameFinish)
    {
        Console.WriteLine($"Correct word: {currentGame.GetSecretWord()}");
        scoreTracker.AddWrongGuessCount(currentGame.GetWrongGuessedLetters().Count);
        DisplayScoreboard();
    }
}

void DisplayWordInfo()
{
    Console.WriteLine($"\nTurns left: {currentGame.GetTriesLeft()}\nGuessed letters: {currentGame.GuessedLettersToString()}");
    Console.WriteLine(currentGame.UpdateSolutionString());
    Console.WriteLine();
}

void DisplayScoreboard()
{
    Console.WriteLine("\n++++++++++Statistics++++++++++");
    Console.WriteLine("Last 10 games:");
    Console.WriteLine("Wrong guessed ______ Time");
    for (int i = 0; i < scoreTracker.GetGuessCounts().Count; i++)
    {
        Console.WriteLine(scoreTracker.GetGuessCounts()[i] + "______" + scoreTracker.GetTimes()[i]);
    }
}

void DisplayPlayerBoard()
{
    List<Player> playerList = scoreTracker.GetPlayers();
    Console.WriteLine("Players:");
    foreach (Player p in playerList)
    {    
        Console.WriteLine($"Name: {p.GetName()}\nGames won: {p.GetGamesWon()}, win ratio: {p.GetWinRatio()}\n" );
    }
}