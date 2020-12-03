using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootballDashboard.Models.Enums;
using System.Collections.Generic;

namespace FantasyFootbalDashboard.DBConnector.Interfaces
{
    /// <summary>
    /// Repository for interacting with ReferenceGames in the database
    /// </summary>
    public interface IReferenceGameRespository
    {
        /// <summary>
        /// Returns all games for a given set of teams
        /// </summary>
        /// <returns>List of games</returns>
        List<ReferenceGame> GetGamesByTeams(List<NflTeam> teams);

        /// <summary>
        /// Returns all games for a given week
        /// </summary>
        /// <returns>List of games</returns>
        List<ReferenceGame> GetGamesByWeek(int weekNumber);

        /// <summary>
        /// Saves game data to the database by oveerwriting previous data
        /// </summary>
        void OverwriteGames(List<ReferenceGame> gamesToSave);

    }
}
