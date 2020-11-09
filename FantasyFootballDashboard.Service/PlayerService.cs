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

        public List<Player> GetAllUserPlayers()
        {
            var players = new List<Player>();

            foreach(IConnector conn in _connectors)
            {
                players.AddRange(conn.GetActivePlayersForUser());
            }

            return players;
        }
    }
}
