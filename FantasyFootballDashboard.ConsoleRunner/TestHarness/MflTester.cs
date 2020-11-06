using FantasyFootballDashboard.Models.Interface;
using System.Linq;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public class MflTester : ITester
    {
        private readonly IConnector _mflConnector;

        public MflTester(IConnector mflConnector)
        {
            _mflConnector = mflConnector;
        }

        public string ExecuteTest()
        {
            var result = _mflConnector.GetActivePlayersForUser();
            var namesList = result
                .Select(p => p.Name);

            var playerNames = string.Join(",", namesList);

            return ($"Players from My Fantasy League: {playerNames}");
        }
    }
}
