using FantasyFootballDashboard.Models.Interface;
using System.Linq;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public class EspnTester : ITester
    {
        private readonly IConnector _espnConnector;

        public EspnTester(IConnector espnConnector)
        {
            _espnConnector = espnConnector;
        }

        public string ExecuteTest()
        {
            var result = _espnConnector.GetActivePlayersForUser();

            var namesList = result
                .Select(p => p.Name);

            var playerNames = string.Join(",", namesList);

            return ($"Players from ESPN: {playerNames}");
        }
    }
}
