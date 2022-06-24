using Hangman.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class GameController
    {
        public Game _game { get; set; }
        public Player _player { get; set; }
        public Word _word { get; set; }

        public GameController(Player player, Game game, Word word)
        {
            _game = game;
            _player = player;
            _word = word;
        }

        public void NewGame(string secretWord, int wordID, int playerID)
        {
            _game.TriesLeft = 10;
            _game.Turns = 0;
            _game.SecretWord = secretWord;
            _game.AllGuessedLetters = "";
            _game.CorrectGuessedLetters = "";
            _game.WrongGuessedLetters = "";
            _game.Won = false;
            _game.PlayerID = playerID;
            _game.WordID = wordID;
        }

        public void NewPlayer(string name)
        {
            _player.Name = name;
        }

        public void NewWord(string secretWord)
        {
            _word.SecretWord = secretWord;
        }

        public void CheckGuess(char c)
        {
            _game.Turns++;
            if (!_game.AllGuessedLetters.Contains(c)) // .Count(x => x == c) methode
            {
                _game.AllGuessedLetters += c;
                if (_word.SecretWord.Contains(c))
                {
                    foreach (char occ in _word.SecretWord)
                    {
                        if (occ == c)
                        {
                            _game.CorrectGuessedLetters += (c);
                        }
                    }
                }
                else
                {
                    _game.TriesLeft--;
                    _game.WrongGuessedLetters += (c);
                }
            }
        }

        public Boolean IsWon()
        {
            if (_game.CorrectGuessedLetters.Length == _word.SecretWord.Length)
            {
                _game.Won = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean IsLost()
        {
            return _game.TriesLeft <= 0;
        }
    }
}
