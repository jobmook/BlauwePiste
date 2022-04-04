// See https://aka.ms/new-console-template for more information
using Hangman;
using System.Diagnostics;

Game currentGame;

Console.WriteLine("\n++++++++++Welcome to Hangman++++++++++");
do
{
    Console.Clear();
    Console.WriteLine("MENU");
    Console.WriteLine("Press 1 to play a Hangman game.");
    Console.WriteLine("Press 2 to to see the high scores.");
    Console.WriteLine("Press q to quit the program.");
    string input = Console.ReadLine();
    if (input.Equals("1"))
    {
        Console.Clear();
        Console.WriteLine("Starting game..");
        Console.WriteLine("Enter a secret word: ");
        string sw = Console.ReadLine();
        currentGame = new Game(sw.Trim().ToLower());
        gameController();
    }
    else if (input.Equals("2"))
    {
        Console.Clear();
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

    if (currentGame.Win())
    {
        DisplayWordInfo();
        Console.WriteLine("You win!");
        Console.WriteLine($"Correct word: {currentGame.GetSecretWord()}");
    }

    if (currentGame.Lose())
    {
        DisplayWordInfo();
        Console.WriteLine("You lose!");
        Console.WriteLine($"Correct word: {currentGame.GetSecretWord()}");    }
}

void DisplayWordInfo()
{
    Console.WriteLine($"\nTurns left: {currentGame.GetTriesLeft()}\nGuessed letters: {currentGame.GuessedLettersToString()}");
    Console.WriteLine(currentGame.UpdateSolutionString());
    Console.WriteLine();
}