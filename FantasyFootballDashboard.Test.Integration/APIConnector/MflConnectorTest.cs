using FantasyFootballDashboard.APIConnector.MFL;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
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
        public async Task GetActivePlayers_GetsPlayers()
        {
            // Arrange
            var connector = new MflConnector(_testProfile.Year, _testProfile.MflUsername, _testProfile.MflPassword);

            // Act
            var players = await connector.GetActivePlayers();

            // Assert
            Assert.IsTrue(players.Any());
            Assert.IsFalse(string.IsNullOrEmpty(players[0].Name));
            Assert.AreNotEqual(Position.None, players[0].Position);
            Assert.AreNotEqual(NflTeam.FreeAgent, players[0].Team);
            Assert.AreEqual(ServiceOption.MyFantasyLeague, players[0].Service[0]);
        }
    }
}
