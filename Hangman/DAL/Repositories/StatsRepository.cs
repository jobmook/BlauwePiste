using Hangman.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman.DAL.Repositories
{
    public class StatsRepository
    {
        public async Task<IEnumerable<Game>> ReturnLast10GamesAsync()
        {
            using (GameContext context = new GameContext())
            {
                var stats = await context.Games.OrderByDescending(k => k.GameID).Take(10).ToListAsync();
                return stats;
            }
        }

        public  PlayerStatistics ReturnBestMostGuessed()
        {
            using (GameContext context = new GameContext())
            {
                IEnumerable<PlayerStatistics> groupedPlayers =
                                     (from g in context.Games

                                      join p in context.Players on g.PlayerID equals p.PlayerID
                                      let wincount = context.Games.Count(x => x.Status == GameStatus.Won && x.PlayerID == p.PlayerID)
                                      let lostcount = context.Games.Count(x => x.Status == GameStatus.Lost && x.PlayerID == p.PlayerID)
                                      let inprogresscount = context.Games.Count(x => x.Status == GameStatus.InProgress && x.PlayerID == p.PlayerID)
                                      let ratio = 100 * (wincount / (wincount + (double)lostcount + (double)inprogresscount))
                                      orderby wincount descending
                                      select new PlayerStatistics()
                                      {
                                          PlayerID = p.PlayerID,
                                          Name = p.Name,//context.Players.First( x => x.PlayerID == gamesByPlayer.Key).Name,
                                          WinCount = wincount,

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
                                      let inprogresscount = context.Games.Count(x => x.Status == GameStatus.InProgress && x.PlayerID == p.PlayerID)
                                      let ratio = 100 * (wincount / (wincount + (double)lostcount + (double)inprogresscount))
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
