using FantasyFootballDashboard.APIConnector.ESPN.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;

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
            mappedPlayer.Service.Add(ServiceOption.ESPN);
            mappedPlayer.ServiceIDs.Add(ServiceOption.ESPN, player.EspnPlayerId);

            return mappedPlayer;
        }
    }
}
