using FantasyFootballDashboard.APIConnector.CBS;
using FantasyFootballDashboard.APIConnector.ESPN;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.MFL;
using FantasyFootballDashboard.Models;
using System.Collections.Generic;

namespace FantasyFootballDashboard.Service
{
    public class ConnectionGenerator
    {
        private readonly UserProfile _userProfile;

        public ConnectionGenerator(UserProfile userProfile)
        {
            _userProfile = userProfile;
        }

        public List<IConnector> GenerateConnectionsFromUserProfile()
        {
            var connectors = new List<IConnector>();

            if(!string.IsNullOrEmpty(_userProfile.CbsUsername) && !string.IsNullOrEmpty(_userProfile.CbsLeagueName))
            {
                try
                {
                    var cbsConnector = new CbsConnector(_userProfile.CbsLeagueName, _userProfile.CbsUsername);
                    connectors.Add(cbsConnector);
                }
                catch
                {
                    // Hook this up to logging once we start doing that. 
                }
            }

            if (!string.IsNullOrEmpty(_userProfile.EspnTeamId) && !string.IsNullOrEmpty(_userProfile.EspnLeagueId))
            {
                try
                {
                    var espnConnector = new EspnConnector(_userProfile.Year, _userProfile.EspnLeagueId, _userProfile.EspnTeamId);
                    connectors.Add(espnConnector);
                }
                catch
                {
                    // Hook this up to logging once we start doing that. 
                }
            }

            if (!string.IsNullOrEmpty(_userProfile.MflUsername) && !string.IsNullOrEmpty(_userProfile.MflPassword))
            {
                try
                {
                    var mflConnector = new MflConnector(_userProfile.Year, _userProfile.MflUsername, _userProfile.MflPassword);
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
