using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Service
{
    /// <summary>
    /// Service used to consolidate and return players from target Fatnasy Football services
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private readonly IEnumerable<IConnector> _connectors;

        /// <summary>
        /// Generate service with desired connections to Fantasy Football services
        /// </summary>
        /// <param name="connectors">Collection of connector objects to desired Fantasy Football services</param>
        public PlayerService(IEnumerable<IConnector> connectors)
        {
            _connectors = connectors;
        }

        /// <summary>
        /// Returns all players from target Fantasy Football services
        /// </summary>
        /// <returns>Collection of player objects</returns>
        public async Task<IEnumerable<Player>> GetAllUserPlayers()
        {
            var players = new List<Player>();

            foreach(IConnector conn in _connectors)
            {
                var playerResponse = await conn.GetActivePlayersForUser();
                players.AddRange(playerResponse);
            }

            return players;
        }
    }
}
