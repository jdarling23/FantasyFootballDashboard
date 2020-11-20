using FantasyFootbalDashboard.DBConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
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
        private readonly IReferencePlayerRepository _refPlayerRepo;

        /// <summary>
        /// Generate service with desired connections to Fantasy Football services
        /// </summary>
        /// <param name="connectors">Collection of connector objects to desired Fantasy Football services</param>
        /// <param name="refPlayerRepo">Reference player repository to assist filling in missing details for a player</param>
        public PlayerService(IEnumerable<IConnector> connectors, IReferencePlayerRepository refPlayerRepo)
        {
            _connectors = connectors;
            _refPlayerRepo = refPlayerRepo;
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

                foreach(var player in playerResponse)
                {
                    // Fill in missing data iff API data is incomplete
                    if (player.Position == Position.None || player.Team == NflTeam.FreeAgent)
                    {

                        var syncedPlayer = SyncPlayerToDatabase(player);
                        players.Add(syncedPlayer);
                    }
                    else
                    {
                        players.Add(player);
                    }
                }
            }

            return players;
        }

        private Player SyncPlayerToDatabase(Player player)
        {
            // Get the additional data
            var refPlayer = _refPlayerRepo.GetReferencePlayer(player);

            // Update the player to return
            player.Position = (Position)refPlayer.Position;
            player.Team = (NflTeam)refPlayer.Team;

            // Save off the service related ID for this player; imrpoves reference data over time
            foreach (var service in player.ServiceIDs)
            {
                switch (service.Key)
                {
                    case (ServiceOption.ESPN):
                        refPlayer.EspnPlayerId = service.Value;
                        _refPlayerRepo.SavePlayer(refPlayer);
                        break;
                    case (ServiceOption.MyFantasyLeague):
                        refPlayer.MyFantasyLeagePlayerId = service.Value;
                        _refPlayerRepo.SavePlayer(refPlayer);
                        break;
                    default:
                        break;
                }
            }

            return player;
        }
    }
}
