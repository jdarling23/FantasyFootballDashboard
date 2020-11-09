using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IEnumerable<IConnector> _connectors;

        public PlayerService(IEnumerable<IConnector> connectors)
        {
            _connectors = connectors;
        }

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
