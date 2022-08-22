using Hangman;
using Hangman.DAL;
using Hangman.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangmanWebApp.Controllers.API
{
    [Route("api/stats")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private GamesRepository _gameRepository;
        private PlayersRepository _playerRepository;
        private WordsRepository _wordRepository;
        private StatsRepository _statsRepository;

        public StatsController(GamesRepository gameRepository, PlayersRepository playerRepository, WordsRepository wordRepository, StatsRepository statsRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _wordRepository = wordRepository;
            _statsRepository = statsRepository;
        }

        [HttpGet]
        [Route("statistics")]
        public async Task<IEnumerable<Game>> GetStats()
        {
            return await _statsRepository.ReturnLast10GamesAsync();
        }

        [HttpGet]
        [Route("highscores/best-most-guessed")]
        public async Task<ActionResult<PlayerStatistics>> GetHighScoresMostGuessed()
        {
            return _statsRepository.ReturnBestMostGuessed();
        }

        [HttpGet]
        [Route("highscores/best-ratio")]
        public async Task<ActionResult<PlayerStatistics>> GetHighScoresBestRatio()
        {
            return _statsRepository.ReturnBestRatio();
        }
    }
}
