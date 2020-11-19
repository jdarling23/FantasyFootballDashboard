using FantasyFootballDashboard.APIConnector.CBS.Models;
using FantasyFootballDashboard.Models;

namespace FantasyFootballDashboard.APIConnector.CBS
{
    /// <summary>
    /// Helper class to map player data from CBS to the system player model
    /// </summary>
    public class CbsPlayerMapper
    {
        /// <summary>
        /// Maps CBS player data to system player object
        /// </summary>
        /// <param name="player">CBS player object</param>
        /// <returns>System player object</returns>
        public Player ConvertPlayer(CbsPlayer player)
        {
            var mappedPlayer = new Player();

            mappedPlayer.Name = player.FullName;

            return mappedPlayer;
        }
    }
}
