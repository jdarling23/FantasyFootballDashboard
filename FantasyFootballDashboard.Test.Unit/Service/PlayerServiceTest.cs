using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
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
                Team = NflTeam.DallasCowboys,
                Position = Position.QuarterBack,
                Service = new List<ServiceOption> { ServiceOption.CBS }
            };

            var playerTwo = new Player()
            {
                Name = "Michael Gallup",
                Team = NflTeam.DallasCowboys,
                Position = Position.WideReceiver,
                Service = new List<ServiceOption> { ServiceOption.ESPN }
            };

            var playerThree = new Player()
            {
                Name = "Marion Barber",
                Team = NflTeam.DallasCowboys,
                Position = Position.RunningBack,
                Service = new List<ServiceOption> { ServiceOption.ESPN }
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
