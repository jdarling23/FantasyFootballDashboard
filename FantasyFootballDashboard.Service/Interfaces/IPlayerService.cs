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
        /// Returns all players from target Fantasy Football services
        /// </summary>
        /// <returns>Collection of player objects</returns>
        Task<IEnumerable<Player>> GetAllUserPlayers();
    }
}
