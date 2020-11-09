using FantasyFootballDashboard.APIConnector.CBS;
using FantasyFootballDashboard.APIConnector.ESPN;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.MFL;
using FantasyFootballDashboard.Models;
using System.Collections.Generic;

namespace FantasyFootballDashboard.Service
{
    public static class ConnectionGenerator
    {
        public static List<IConnector> GenerateConnectionsFromUserProfile(UserProfile userProfile)
        {
            var connectors = new List<IConnector>();

            if(!string.IsNullOrEmpty(userProfile.CbsUsername) && !string.IsNullOrEmpty(userProfile.CbsLeagueName))
            {
                try
                {
                    var cbsConnector = new CbsConnector(userProfile.CbsLeagueName, userProfile.CbsUsername);
                    connectors.Add(cbsConnector);
                }
                catch
                {
                    // Hook this up to logging once we start doing that. 
                }
            }

            if (!string.IsNullOrEmpty(userProfile.EspnTeamId) && !string.IsNullOrEmpty(userProfile.EspnLeagueId))
            {
                try
                {
                    var espnConnector = new EspnConnector(userProfile.Year, userProfile.EspnLeagueId, userProfile.EspnTeamId);
                    connectors.Add(espnConnector);
                }
                catch
                {
                    // Hook this up to logging once we start doing that. 
                }
            }

            if (!string.IsNullOrEmpty(userProfile.MflUsername) && !string.IsNullOrEmpty(userProfile.MflPassword))
            {
                try
                {
                    var mflConnector = new MflConnector(userProfile.Year, userProfile.MflUsername, userProfile.MflPassword);
                    connectors.Add(mflConnector);
                }
                catch
                {
                    // Hook this up to logging once we start doing that. 
                }
            }

            return connectors;
        }
    }
}
