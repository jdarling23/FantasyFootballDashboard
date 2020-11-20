using FantasyFootbalDashboard.DBConnector.Interfaces;
using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace FantasyFootbalDashboard.DBConnector.Repositories
{
    /// <summary>
    /// Manages CRUD operations for players in reference players table
    /// </summary>
    public class ReferencePlayerRepository : IReferencePlayerRepository
    {
        private readonly FantasyFootballDashboardContext _dbContext;

        public ReferencePlayerRepository(FantasyFootballDashboardContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Returns a player with a given ESPN Id
        /// </summary>
        /// <param name="espnId">ID of the player in ESPN</param>
        /// <returns>Reference Player model, or null if none found</returns>
        public ReferencePlayer GetPlayerByEspnId(int espnId)
        {
            var result = _dbContext.ReferencePlayers
                .Where(rp => rp.EspnPlayerId == espnId);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns a reference player associated to a system player model
        /// </summary>
        /// <param name="player">System player object</param>
        /// <returns>Reference Player model, or null if none found</returns>
        public ReferencePlayer GetReferencePlayer(Player player)
        {
            var result = _dbContext.ReferencePlayers
                .Where(rp => rp.Name.Equals(player.Name));

            if (result == null)
            {
                GetPlayerByServiceId(player.ServiceIDs);
            }

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Returns a player based on provided service IDs. Returns the first found in the provided dictionary
        /// </summary>
        /// <param name="serviceIds">Dictionary of Service and the associated ID</param>
        /// <returns>Reference Player model, or null if none found</returns>
        public ReferencePlayer GetPlayerByServiceId(Dictionary<ServiceOption, int> serviceIds)
        {
            var result = default(ReferencePlayer);

            foreach (var serviceId in serviceIds)
            {
                switch (serviceId.Key)
                {
                    case (ServiceOption.ESPN):
                        result = _dbContext.ReferencePlayers
                            .Where(rp => rp.EspnPlayerId == serviceId.Value)
                            .FirstOrDefault();
                        break;
                    case (ServiceOption.MyFantasyLeague):
                        result = _dbContext.ReferencePlayers
                            .Where(rp => rp.MyFantasyLeagePlayerId == serviceId.Value)
                            .FirstOrDefault();
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Saves a Reference Player to the database
        /// </summary>
        /// <param name="player">Reference palyer model</param>
        public void SavePlayer(ReferencePlayer player)
        {
            _dbContext.ReferencePlayers.Update(player);
            _dbContext.SaveChanges();
        }
    }
}
