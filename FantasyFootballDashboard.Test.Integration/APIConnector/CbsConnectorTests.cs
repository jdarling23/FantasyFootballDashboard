using FantasyFootballDashboard.APIConnector.CBS;
using FantasyFootballDashboard.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Test.Integration.APIConnector
{
    [TestClass]
    public class CbsConnectorTests
    {
        private UserProfile _testProfile;

        [TestInitialize]
        public void Init()
        {
            _testProfile = ConfigurationHelper.GetTestProfile();
        }

        // CBS is on the fritz at this current time
        [TestMethod, Ignore]
        public async Task GetActivePlayers_GetsPlayers()
        {
            // Arrange
            var connector = new CbsConnector(_testProfile.CbsLeagueName, _testProfile.CbsUsername);

            // Act
            var players = await connector.GetActivePlayers();

            // Assert
            Assert.IsTrue(players.Any());
            Assert.IsFalse(string.IsNullOrEmpty(players[0].Name));
        }
    }
}
