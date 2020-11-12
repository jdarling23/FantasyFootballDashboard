using FantasyFootballDashboard.APIConnector.MFL;
using FantasyFootballDashboard.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Test.Integration.APIConnector
{
    [TestClass]
    public class MflConnectorTest
    {
        private UserProfile _testProfile;

        [TestInitialize]
        public void Init()
        {
            _testProfile = ConfigurationHelper.GetTestProfile();
        }

        [TestMethod, Ignore]
        public async Task GetActivePlayersForUser_GetsPlayers()
        {
            // Arrange
            var connector = new MflConnector(_testProfile.Year, _testProfile.MflUsername, _testProfile.MflPassword);

            // Act
            var players = await connector.GetActivePlayersForUser();

            // Assert
            Assert.IsTrue(players.Any());
            Assert.IsFalse(string.IsNullOrEmpty(players[0].Name));
        }
    }
}
