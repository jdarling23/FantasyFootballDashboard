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
        public async Task GetReferencePlayerData_ReturnsPlayer()
        {
            // Arrange
            var connector = new SportsDataConnector(_apiKey);

            // Act
            var players = await connector.GetRefPlayerData();

            // Assert
            Assert.IsTrue(players.Any());
        }
    }
}
