using FantasyFootballDashboard.APIConnector.CBS.Models;
using FantasyFootballDashboard.Models;

namespace FantasyFootballDashboard.APIConnector.CBS
{
    public class CbsPlayerMapper
    {
        public Player ConvertPlayer(CbsPlayer player)
        {
            var mappedPlayer = new Player();

            mappedPlayer.Name = player.FullName;

            return mappedPlayer;
        }
    }
}
