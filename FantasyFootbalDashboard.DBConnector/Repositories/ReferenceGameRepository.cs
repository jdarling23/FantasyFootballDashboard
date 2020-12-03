using FantasyFootbalDashboard.DBConnector.Interfaces;
using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootballDashboard.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace FantasyFootbalDashboard.DBConnector.Repositories
{
    public class ReferenceGameRepository : IReferenceGameRespository
    {
        private readonly FantasyFootballDashboardContext _dbContext;

        public ReferenceGameRepository(FantasyFootballDashboardContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Returns all games for a given set of teams
        /// </summary>
        /// <returns>List of games</returns>
        public List<ReferenceGame> GetGamesByTeams(List<NflTeam> teams)
        {
            var result = _dbContext.ReferenceGames
                .Where(g => teams.Contains((NflTeam)g.HomeTeam) || teams.Contains((NflTeam)g.AwayTeam))
                .ToList();

            return result;
        }

        /// <summary>
        /// Returns all games for a given week
        /// </summary>
        /// <returns>List of games</returns>
        public List<ReferenceGame> GetGamesByWeek(int weekNumber)
        {
            var result = _dbContext.ReferenceGames
                .Where(g => g.Week == weekNumber)
                .ToList();

            return result;
        }

        /// <summary>
        /// Saves game data to the database by oveerwriting previous data
        /// </summary>
        public void OverwriteGames(List<ReferenceGame> gamesToSave)
        {
            _dbContext.ReferenceGames.AddRange(gamesToSave);
            _dbContext.SaveChanges();
        }
    }
}
