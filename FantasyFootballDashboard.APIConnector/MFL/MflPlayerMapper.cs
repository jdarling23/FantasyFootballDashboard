using FantasyFootballDashboard.APIConnector.MFL.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;

namespace FantasyFootballDashboard.APIConnector.MFL
{
    /// <summary>
    /// Helper class to map player data from My Fantasy League to the system player model
    /// </summary>
    public class MflPlayerMapper
    {
        /// <summary>
        /// Maps My Fantasy League player data to system player object
        /// </summary>
        /// <param name="player">MFL player object</param>
        /// <returns>System player object</returns>
        public Player ConvertPlayer(MflPlayer player)
        {
            var mappedPlayer = new Player();

            mappedPlayer.Name = player.FullName;
            mappedPlayer.Position = PositionMapper.MapPosition(player.Position);
            mappedPlayer.Team = TeamMapper.MapTeam(player.NflTeam);
            mappedPlayer.Service.Add(ServiceOption.MyFantasyLeague);
            mappedPlayer.ServiceIDs.Add(ServiceOption.MyFantasyLeague, int.Parse(player.PlayerId));

            return mappedPlayer;
        }
    }
}
