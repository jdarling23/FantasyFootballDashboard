using FantasyFootballDashboard.APIConnector.ESPN.Models;
using FantasyFootballDashboard.Models;

namespace FantasyFootballDashboard.APIConnector.ESPN
{
    public class EspnPlayerMapper
    {
        public Player ConvertPlayer(EspnPlayer player)
        {
            var mappedPlayer = new Player();
            mappedPlayer.Name = player.FullName;

            return mappedPlayer;
        }
    }
}
