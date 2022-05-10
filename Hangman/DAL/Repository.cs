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
       public static Word GetSecretWord()
        {
            using(GameContext context = new GameContext())
            {
                var word = context.Words.OrderBy(r => Guid.NewGuid()).First();
                return word;
            }
        }

        public static IEnumerable<Game> AllGames()
        {
            using (GameContext context = new GameContext())
            {
                return context.Games.ToList();
            }
        }

        public static void AddGame(Game game)
        {
            using (GameContext context = new GameContext())
            {
                context.Games.Add(game);
                context.SaveChanges();
            }
        }

        public static IEnumerable<Player> AllPlayers()
        {
            using (GameContext context = new GameContext())
            {
                return context.Players.ToList();
            }
        }

        public static Boolean IsEmptyWordList()
        {
            using(GameContext context = new GameContext())
            {
                var wordlist = context.Words.ToList();
                return wordlist.Count() == 0;
            }
        }

        public static void AddPlayer(Player player)
        {
            using (GameContext context = new GameContext())
            {
                context.Players.Add(player);
                context.SaveChanges();
            }
        }

        public static void RegisterWonGame(Player player)
        {
            using (GameContext context = new GameContext())
            {
                context.Players.Add(player);
                context.SaveChanges();
            }
        }

        public static List<Game> ReturnLast10Games()
        {
            using(GameContext context = new GameContext())
            {
                var stats = context.Games.OrderByDescending(k => k.GameID).Take(10).ToList();
                return stats;
            }
        }

        public static PlayerStatistics ReturnBestMostGuessed()
        {
            using (GameContext context = new GameContext())
            {
                IEnumerable<PlayerStatistics> groupedPlayers =
                                     (from g in context.Games

                                      join p in context.Players on g.PlayerID equals p.PlayerID
                                      let wincount = context.Games.Count(x => x.Won == true && x.PlayerID == p.PlayerID)
                                      let lostcount = context.Games.Count(x => x.Won == false && x.PlayerID == p.PlayerID)
                                      let ratio = 100 * (wincount / (wincount + (double) lostcount))
                                      orderby wincount descending
                                      select new PlayerStatistics()
                                      {
                                          PlayerID = p.PlayerID,
                                          Name = p.Name,//context.Players.First( x => x.PlayerID == gamesByPlayer.Key).Name,
                                          WinCount = wincount,
                                          LostCount = lostcount,
                                          Ratio = ratio
                                      });
                return groupedPlayers.First();
            }
        }

        public static PlayerStatistics ReturnBestRatio()
        {
            using (GameContext context = new GameContext())
            {

                IEnumerable<PlayerStatistics> groupedPlayers =
                                     (from g in context.Games
                                      join p in context.Players on g.PlayerID equals p.PlayerID
                                      let wincount = context.Games.Count(x => x.Won == true && x.PlayerID == p.PlayerID)
                                      let lostcount = context.Games.Count(x => x.Won == false && x.PlayerID == p.PlayerID)
                                      let ratio = 100 * (wincount / (wincount + (double)lostcount))
                                      orderby ratio descending
                                      select new PlayerStatistics()
                                      {
                                          PlayerID = p.PlayerID,
                                          Name = p.Name,//context.Players.First( x => x.PlayerID == gamesByPlayer.Key).Name,
                                          WinCount = wincount,
                                          LostCount = lostcount,
                                          Ratio = ratio
                                      });
                return groupedPlayers.First();
            }


        }
    }
}
