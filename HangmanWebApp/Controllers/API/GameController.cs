using Hangman;
using Hangman.DAL;
using Hangman.DAL.Repositories;
using Hangman.Domain;
using HangmanWebApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanWebApp.Controllers.API
{

    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private GamesRepository _gameRepository;
        private PlayersRepository _playerRepository;
        private WordsRepository _wordRepository;
        private StatsRepository _statsRepository;

        public GameController(GamesRepository gameRepository, PlayersRepository playerRepository, WordsRepository wordRepository, StatsRepository statsRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _wordRepository = wordRepository;
            _statsRepository = statsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Game>> Get(int id)
        {
            var game = await _gameRepository.GetGameByIdAsync(id);
            return game == null ? NotFound() : game;
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Post(NewGameDTO newGameDto)
        {
            var player = await _playerRepository.GetPlayerByNameAsync(newGameDto.PlayerName);
            player ??= await _playerRepository.CreatePlayer(newGameDto.PlayerName);

            Word newWord = _wordRepository.GetSecretWordAsync().Result;

            Game newGame = new Game();

            newGame.TriesLeft = 10;
            newGame.Turns = 0;
            newGame.SecretWord = newWord.SecretWord;
            newGame.AllGuessedLetters = "";
            newGame.CorrectGuessedLetters = "";
            newGame.WrongGuessedLetters = "";
            newGame.PlayerID = player.PlayerID;
            newGame.WordID = newWord.WordID;
            newGame.Status = GameStatus.InProgress;
            newGame.StartTime = DateTime.Now.Millisecond;
            await _gameRepository.AddGameAsync(newGame);
            return await _gameRepository.GetLastGameAsync();
        }

        [HttpPost]
        [Route("guess")]
        public async Task<ActionResult<Game>> Guess(GuessDTO guessDto)
        {
            int gameId = guessDto.GameId;
            char guessedLetter = guessDto.guessedLetter;
            Game currentGame =  await _gameRepository.GetGameByIdAsync(gameId);
            currentGame.Turns++;
            if (!currentGame.AllGuessedLetters.Contains(guessedLetter))
            {
                currentGame.AllGuessedLetters += guessedLetter;
                if (currentGame.SecretWord.Contains(guessedLetter))
                {
                    foreach (char occ in currentGame.SecretWord)
                    {
                        if (occ == guessedLetter)
                        {
                            currentGame.CorrectGuessedLetters += (guessedLetter);
                        }
                    }
                }
                else
                {
                    currentGame.TriesLeft--;
                    currentGame.WrongGuessedLetters += (guessedLetter);

                }

                if (UpdateUI.IsWon(currentGame))
                {
                    currentGame.EndTime = DateTime.Now.Millisecond;
                    currentGame.Status = GameStatus.Won;
                }

                if (UpdateUI.IsLost(currentGame))
                {
                    currentGame.EndTime = DateTime.Now.Millisecond;
                    currentGame.Status = GameStatus.Lost;

                }
                await _gameRepository.UpdateGameAsync(currentGame);
            }
            return _gameRepository.GetGameByIdAsync(gameId).Result;
            // string SolutionString = UpdateSolutionString(currentGame);
        }
    }



    public class UpdateUI
    {
        public static string UpdateSolutionString(Game currentGame) //ui
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

        public static string GuessedLettersToString(Game currentGame) //ui
        {
            string res = "";
            foreach (char c in currentGame.AllGuessedLetters)
            {
                res += c;
                res += ", ";
            }
            return res;
        }

        public static Boolean IsWon(Game currentGame)
        {
            return currentGame.CorrectGuessedLetters.Length == currentGame.SecretWord.Length;
        }

        public static Boolean IsLost(Game currentGame)
        {
            return currentGame.TriesLeft <= 0;
        }
    }
}
