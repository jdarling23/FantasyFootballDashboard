using FantasyFootballDashboard.APIConnector.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public class MflTester : ITester
    {
        private readonly IFantasyConnector _mflConnector;

        public MflTester(IFantasyConnector mflConnector)
        {
            _mflConnector = mflConnector;
        }

        public async Task<string> ExecuteTest()
        {
            var result = await _mflConnector.GetActivePlayers();
            var namesList = result
                .Select(p => p.Name);

            var playerNames = string.Join(",", namesList);

            return ($"Players from My Fantasy League: {playerNames}");
        }
    }
}
