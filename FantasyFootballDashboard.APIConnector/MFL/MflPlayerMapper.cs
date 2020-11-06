using FantasyFootballDashboard.APIConnector.MFL.Models;
using FantasyFootballDashboard.Models;

namespace FantasyFootballDashboard.APIConnector.MFL
{
    public class MflPlayerMapper
    {
        public Player ConvertPlayer(MflPlayer player)
        {
            var mappedPlayer = new Player();

            mappedPlayer.Name = player.FullName;

            return mappedPlayer;
        }
    }
}
