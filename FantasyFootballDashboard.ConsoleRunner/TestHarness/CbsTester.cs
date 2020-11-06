using FantasyFootballDashboard.Models.Interface;
using System.Text;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public class CbsTester : ITester
    {
        private readonly IConnector _cbsConnector;

        public CbsTester(IConnector cbsConnector)
        {
            _cbsConnector = cbsConnector;
        }

        public string ExecuteTest()
        {
            var result = _cbsConnector.GetActivePlayersForUser();

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