using FantasyFootballDashboard.APIConnector.ESPN.Models;
using FantasyFootballDashboard.Models;

namespace FantasyFootballDashboard.APIConnector.ESPN
{
    /// <summary>
    /// Helper class to map player data from ESPN to the system player model
    /// </summary>
    public class EspnPlayerMapper
    {
        /// <summary>
        /// Maps ESPN player data to system player object
        /// </summary>
        /// <param name="player">ESPN player object</param>
        /// <returns>System player object</returns>
        public Player ConvertPlayer(EspnPlayer player)
        {
            var mappedPlayer = new Player();
            mappedPlayer.Name = player.FullName;

            return mappedPlayer;
        }
    }
}
