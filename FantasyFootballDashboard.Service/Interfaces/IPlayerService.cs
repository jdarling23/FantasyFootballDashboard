using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Service.Interfaces
{
    /// <summary>
    /// Service used to consolidate and return players from target Fatnasy Football services
    /// </summary>
    public interface IPlayerService
    {
        /// <summary>
        /// Allows you to add additional connectors to the service
        /// </summary>
        /// <param name="connToAdd">Connection to add</param>
        void AddConnector(IFantasyConnector connToAdd);

        /// <summary>
        /// Returns all players from target Fantasy Football services
        /// </summary>
        /// <returns>Collection of player objects</returns>
        Task<IList<Player>> GetAllUserPlayers();
    }
}
