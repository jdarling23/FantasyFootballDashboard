using FantasyFootballDashboard.APIConnector.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public class CbsTester : ITester
    {
        private readonly IConnector _cbsConnector;

        public CbsTester(IConnector cbsConnector)
        {
            _cbsConnector = cbsConnector;
        }

        public async Task<string> ExecuteTest()
        {
            var result = await _cbsConnector.GetActivePlayers();

            var playerNames = new StringBuilder();
            playerNames.Append("Players from CBS: ");

            foreach(var player in result)
            {
                playerNames.Append($"{player.Name}, ");
            }

            return playerNames.ToString();
        }
    }
}