using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Service.Interfaces;
using System.Collections.Generic;

namespace FantasyFootballDashboard.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IEnumerable<IConnector> _connectors;

        public PlayerService(IEnumerable<IConnector> connectors)
        {
            _connectors = connectors;
        }

        public IEnumerable<Player> GetAllUserPlayers()
        {
            var players = new List<Player>();

            // Need to come back and make these connectors async going forward
            foreach(IConnector conn in _connectors)
            {
                players.AddRange(conn.GetActivePlayersForUser());
            }

            return players;
        }
    }
}
