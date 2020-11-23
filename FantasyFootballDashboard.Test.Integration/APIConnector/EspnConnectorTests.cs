using FantasyFootballDashboard.APIConnector.ESPN;
using FantasyFootballDashboard.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Test.Integration.APIConnector
{
    [TestClass]
    public class EspnConnectorTests
    {
        private UserProfile _testProfile;

        [TestInitialize]
        public void Init()
        {
            _testProfile = ConfigurationHelper.GetTestProfile();
        }

        [TestMethod, Ignore]
        public async Task GetActivePlayers_GetsPlayers()
        {
            // Arrange
            var connector = new EspnConnector(_testProfile.Year, _testProfile.EspnLeagueId, _testProfile.EspnTeamId);

            // Act
            var players = await connector.GetActivePlayers();

            // Assert
            Assert.IsTrue(players.Any());
            Assert.IsFalse(string.IsNullOrEmpty(players[0].Name));
        }
    }
}
