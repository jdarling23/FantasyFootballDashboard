using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Test.Unit.Service
{
    [TestClass]
    public class PlayerServiceTest
    {
        [TestMethod]
        public async Task GetAllUserPlayers_ReturnsPlayers()
        {
            // Arrange
            var connectorOne = new Mock<IConnector>();
            var connectorTwo = new Mock<IConnector>();

            var playerOne = new Player()
            {
                Name = "Dak Prescott",
            };

            var playerTwo = new Player()
            {
                Name = "Michael Gallup",
            };

            var playerThree = new Player()
            {
                Name = "Marion Barber",
            };

            connectorOne.Setup(c => c.GetActivePlayersForUser())
                .ReturnsAsync(new List<Player>() { playerOne });

            connectorTwo.Setup(c => c.GetActivePlayersForUser())
                .ReturnsAsync(new List<Player>() { playerTwo, playerThree });

            var playerService = new PlayerService
                (new List<IConnector>() { connectorOne.Object, connectorTwo.Object });

            // Act
            var result = (await playerService.GetAllUserPlayers())
                .ToList();

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result
                .Select(p => p.Name)
                .Contains("Dak Prescott"));
            Assert.IsTrue(result
                .Select(p => p.Name)
                .Contains("Michael Gallup"));
            Assert.IsTrue(result
                .Select(p => p.Name)
                .Contains("Marion Barber"));
        }
    }
}
