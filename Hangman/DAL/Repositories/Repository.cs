using Hangman.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL
{
    public class Repository
    {
        public Game ReturnGameOnID(int id)
        {
            using(GameContext context = new GameContext())
            {
                return context.Games.Find(id);
            }
        }

        public Player GetPlayerById(int id)
        {
            using (GameContext context = new GameContext())
            {
                return context.Players.Find(id);
            }
        }

        public void UpdateGameAfterWrongGuess(int gameId, char c)
        {
            using(GameContext context = new GameContext())
            {
                var game = context.Games.Find(gameId);
                game.WrongGuessedLetters += c;
                game.TriesLeft--;
                Console.WriteLine($"Game {0} updated: added wrong letter{1}",game.GameID, c);
            }
        }

        public void UpdateAfterCorrectGuess(int gameId, char c)
        {
            using (GameContext context = new GameContext())
            {
                var game = context.Games.Find(gameId);
                game.CorrectGuessedLetters += c;
                Console.WriteLine($"Game {0} updated: added correct letter{1}", game.GameID, c);
            }
        }

        public void UpdateAfterWonGame(int gameId)
        {
            using (GameContext context = new GameContext())
            {
                var game = context.Games.Find(gameId);
                game.Status = GameStatus.Won;
                Console.WriteLine($"Game is won");
            }
        }

        public void UpdateAfterLostGame(int gameId)
        {
            using (GameContext context = new GameContext())
            {
                var game = context.Games.Find(gameId);
                game.Status = GameStatus.Lost;
                Console.WriteLine($"Game is lost");
            }
        }

        public string ReturnCorrectGuessedLettersOnId(int gameId)
        {
            using(GameContext context = new GameContext())
            {
                var res = context.Games.Find(gameId);
                return res.CorrectGuessedLetters;
            }
        }

        public int ReturnsNumberOfTurnsOnId(int gameId)
        {
            using (GameContext context = new GameContext())
            {
                var res = context.Games.Find(gameId);
                return res.Turns;
            }
        }

        public string ReturnSecretWordOnId(int gameId)
        {
            using (GameContext context = new GameContext())
            {
                var res = context.Games.Find(gameId);
                return res.SecretWord;
            }
        }

        public string ReturnWrongGuessedLettersOnId(int gameId)
        {
            using (GameContext context = new GameContext())
            {
                var res = context.Games.Find(gameId);
                return res.WrongGuessedLetters;
            }
        }

        public int ReturnTurnsRemainingOnId(int gameId)
        {
            using (GameContext context = new GameContext())
            {
                var res = context.Games.Find(gameId);
                return res.TriesLeft;
            }
        }



        public Word ReturnWordOnId(int id)
        {
            using (GameContext context = new GameContext())
            {
                return context.Words.Find(id);
            }
        }

        public Word GetSecretWord()
        {
            using (GameContext context = new GameContext())
            {
                var word = context.Words.OrderBy(r => Guid.NewGuid()).First();
                return word;
            }
        }

        public IEnumerable<Game> AllGames()
        {
            using (GameContext context = new GameContext())
            {
                return context.Games.ToList();
            }
        }
      

        public IEnumerable<Player> AllPlayers()
        {
            using (GameContext context = new GameContext())
            {
                return context.Players.ToList();
            }
        }

        public Boolean IsEmptyWordList()
        {
            using(GameContext context = new GameContext())
            {
                var wordlist = context.Words.ToList();
                return wordlist.Count() == 0;
            }
        }


        public void AddPlayer(Player playerToAdd)
        {
            using (GameContext context = new GameContext())
            {
                context.Players.Add(playerToAdd);
                context.SaveChanges();
            }
        }

        public void AddGame(Game gameToAdd)
        {
            using(GameContext context = new GameContext())
            {
                context.Games.Add(gameToAdd);
            }
        }

        

        public List<Game> ReturnLast10Games()
        {
            using(GameContext context = new GameContext())
            {
                var stats = context.Games.OrderByDescending(k => k.GameID).Take(10).ToList();
                return stats;
            }
        }

        public PlayerStatistics ReturnBestMostGuessed()
        {
            using (GameContext context = new GameContext())
            {
                IEnumerable<PlayerStatistics> groupedPlayers =
                                     (from g in context.Games

                                      join p in context.Players on g.PlayerID equals p.PlayerID
                                      let wincount = context.Games.Count(x => x.Status == GameStatus.Won && x.PlayerID == p.PlayerID)
                                 
                                      orderby wincount descending
                                      select new PlayerStatistics()
                                      {
                                          PlayerID = p.PlayerID,
                                          Name = p.Name,//context.Players.First( x => x.PlayerID == gamesByPlayer.Key).Name,
                                          WinCount = wincount
                                      });
                return groupedPlayers.First();
            }
        }

        public PlayerStatistics ReturnBestRatio()
        {
            using (GameContext context = new GameContext())
            {

                IEnumerable<PlayerStatistics> groupedPlayers =
                                     (from g in context.Games
                                      join p in context.Players on g.PlayerID equals p.PlayerID
                                      let wincount = context.Games.Count(x => x.Status == GameStatus.Won && x.PlayerID == p.PlayerID)
                                      let lostcount = context.Games.Count(x => x.Status == GameStatus.Lost && x.PlayerID == p.PlayerID)
                                      let ratio = 100 * (wincount / (wincount + (double)lostcount))
                                      orderby ratio descending
                                      select new PlayerStatistics()
                                      {
                                          PlayerID = p.PlayerID,
                                          Name = p.Name,
                                          WinCount = wincount,
                                          LostCount = lostcount,
                                          Ratio = ratio
                                      });
                return groupedPlayers.First();
            }
        }
    }
}
