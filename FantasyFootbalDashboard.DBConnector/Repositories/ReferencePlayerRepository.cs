﻿using FantasyFootbalDashboard.DBConnector.Interfaces;
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
        /// Returns all reference players in the database
        /// </summary>
        /// <returns>List of reference player models</returns>
        public List<ReferencePlayer> GetAllReferencePlayers()
        {
            return _dbContext.ReferencePlayers
                .ToList();
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
                .Where(rp => rp.Name.Equals(player.Name))
                .FirstOrDefault();

            if (result == null && player.ServiceIDs != null)
            {
                result = GetPlayerByServiceId(player.ServiceIDs);
            }

            return result;
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
                        return result;
                    case (ServiceOption.MyFantasyLeague):
                        result = _dbContext.ReferencePlayers
                            .Where(rp => rp.MyFantasyLeagePlayerId == serviceId.Value)
                            .FirstOrDefault();
                        return result;
                    default:
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Saves Reference Players to the database
        /// </summary>
        /// <param name="players">List of Reference Player models</param>
        public void SavePlayers(List<ReferencePlayer> players)
        {
            var newPlayers = players
                .Where(p => p.ReferencePlayerId == null);

            var existingPlayers = players
                .Where(p => p.ReferencePlayerId != null);

            _dbContext.ReferencePlayers.AddRange(newPlayers);
            _dbContext.ReferencePlayers.UpdateRange(existingPlayers);

            _dbContext.SaveChanges();
        }
    }
}
