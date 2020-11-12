using FantasyFootballDashboard.APIConnector.CBS;
using FantasyFootballDashboard.APIConnector.ESPN;
using FantasyFootballDashboard.APIConnector.MFL;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FantasyFootballDashboard.Test.Integration.Service
{
    [TestClass]
    public class ConnectionGeneratorTests
    {
        private UserProfile _testProfile;

        [TestInitialize]
        public void Init()
        {
            _testProfile = ConfigurationHelper.GetTestProfile();
        }

        [TestMethod]
        public void GenerateConnectionsFromUserProfile()
        {
            // Arrange

            // Act
            var connectors = ConnectionGenerator.GenerateConnectionsFromUserProfile(_testProfile);

            // Assert - CBS Service seems boinked for now, investigating in issue
            //Assert.AreEqual(1, connectors.Where(c => c.GetType() == typeof(CbsConnector)).Count());
            Assert.AreEqual(1, connectors.Where(c => c.GetType() == typeof(EspnConnector)).Count());
            Assert.AreEqual(1, connectors.Where(c => c.GetType() == typeof(MflConnector)).Count());
        }
    }
}
