﻿using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using System.Collections.Generic;

namespace FantasyFootbalDashboard.DBConnector.Interfaces
{
    /// <summary>
    /// Repository for interacting with Reference Player table in the database
    /// </summary>
    public interface IReferencePlayerRepository
    {
        /// <summary>
        /// Returns a reference player associated to a system player model
        /// </summary>
        /// <param name="player">System player object</param>
        /// <returns>Reference Player model, or null if none found</returns>
        ReferencePlayer GetReferencePlayer(Player player);

        /// <summary>
        /// Returns a player based on provided service IDs. Returns the first found in the provided dictionary
        /// </summary>
        /// <param name="serviceIds">Dictionary of Service and the associated ID</param>
        /// <returns>Reference Player model, or null if none found</returns>
        ReferencePlayer GetPlayerByServiceId(Dictionary<ServiceOption, int> serviceIds);

        /// <summary>
        /// Saves a Reference Player to the database
        /// </summary>
        /// <param name="player">Reference palyer model</param>
        void SavePlayer(ReferencePlayer player);
    }
}
