using FantasyFootballDashboard.APIConnector.SportsData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.APIConnector.Interfaces
{
    /// <summary>
    /// Interface for API connectors that specifically pull in NFL player, game, and statistical data
    /// </summary>
    public interface IReferenceConnector
    {
        /// <summary>
        /// Gets all NFL games for a given season 
        /// </summary>
        /// <param name="year">Year for desired seasn (e.g. 2020, 2017, etc)</param>
        /// <returns>List of game objects</returns>
        public Task<List<SportsDataGame>> GetNflGames(string year);

        /// <summary>
        /// Gets reference player data from a service
        /// </summary>
        /// <returns></returns>
        Task<List<ReferencePlayerBase>> GetReferencePlayers();
    }
}
