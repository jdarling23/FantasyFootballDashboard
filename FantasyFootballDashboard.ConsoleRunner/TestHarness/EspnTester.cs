using FantasyFootballDashboard.APIConnector.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public class EspnTester : ITester
    {
        private readonly IConnector _espnConnector;

        public EspnTester(IConnector espnConnector)
        {
            _espnConnector = espnConnector;
        }

        public async Task<string> ExecuteTest()
        {
            var result = await _espnConnector.GetActivePlayersForUser();

            var namesList = result
                .Select(p => p.Name);

            var playerNames = string.Join(",", namesList);

            return ($"Players from ESPN: {playerNames}");
        }
    }
}
