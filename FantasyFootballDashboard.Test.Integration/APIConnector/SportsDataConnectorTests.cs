using FantasyFootballDashboard.APIConnector.SportsData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Test.Integration.APIConnector
{
    [TestClass]
    public class SportsDataConnectorTests
    {
        private string _apiKey;

        [TestInitialize]
        public void Init()
        {
            _apiKey = ConfigurationHelper.GetSportsDataApiKey();
        }

        [TestMethod, Ignore]
        public async Task GetNflGames_ReturnsGames()
        {
            // Arrange
            var connector = new SportsDataConnector(_apiKey);

            // Act
            var games = await connector.GetNflGames("2020");

            // Assert
            Assert.IsTrue(games.Any());
        }

        [TestMethod, Ignore]
        public async Task GetReferencePlayerData_ReturnsPlayer()
        {
            // Arrange
            var connector = new SportsDataConnector(_apiKey);

            // Act
            var players = await connector.GetReferencePlayers();

            // Assert
            Assert.IsTrue(players.Any());
        }
    }
}
