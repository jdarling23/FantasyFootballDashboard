using FantasyFootbalDashboard.DBConnector;
using FantasyFootbalDashboard.DBConnector.Interfaces;
using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootbalDashboard.DBConnector.Repositories;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using FantasyFootballDashboard.Service;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private SqliteConnection _connection;

        protected FantasyFootballDashboardContext _context;

        [TestInitialize]
        public void Init()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<FantasyFootballDashboardContext>()
                    .UseSqlite(_connection)
                    .Options;

            _context = new FantasyFootballDashboardContext(options);
            _context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task GetAllUserPlayers_ReturnsPlayers()
        {
            // Arrange
            var connectorOne = new Mock<IConnector>();
            var connectorTwo = new Mock<IConnector>();
            var refPlayerRepo = new Mock<IReferencePlayerRepository>();

            var refPlayerTwo = new ReferencePlayer()
            {
                Name = "Michael Gallup",
                Position = (int)Position.WideReceiver,
                Team = (int)NflTeam.DallasCowboys,
            };

            var playerOne = new Player()
            {
                Name = "Dak Prescott",
                Team = NflTeam.DallasCowboys,
                Position = Position.QuarterBack,
                Service = new List<ServiceOption> { ServiceOption.MyFantasyLeague }
            };

            var playerTwo = new Player()
            {
                Name = "Michael Gallup",
                Position = Position.None,
                Team = NflTeam.FreeAgent,
                Service = new List<ServiceOption> { ServiceOption.ESPN },
                ServiceIDs = new Dictionary<ServiceOption, int>() { { ServiceOption.ESPN, 12345 } }
            };

            connectorOne.Setup(c => c.GetActivePlayersForUser())
                .ReturnsAsync(new List<Player>() { playerOne });

            connectorTwo.Setup(c => c.GetActivePlayersForUser())
                .ReturnsAsync(new List<Player>() { playerTwo });

            refPlayerRepo.Setup(r => r.GetReferencePlayer(It.IsAny<Player>()))
                .Returns(refPlayerTwo);

            refPlayerRepo.Setup(r => r.SavePlayer(It.IsAny<ReferencePlayer>()))
                .Verifiable();

            var playerService = new PlayerService(
                new List<IConnector>() { connectorOne.Object, connectorTwo.Object },
                refPlayerRepo.Object
            );

            // Act
            var result = (await playerService.GetAllUserPlayers())
                .ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result
                .Select(p => p.Name)
                .Contains("Dak Prescott"));
            Assert.IsTrue(result
                .Select(p => p.Name)
                .Contains("Michael Gallup"));

            var playerTwoTest = result
                .Where(p => p.Name.Equals("Michael Gallup"))
                .First();

            Assert.AreEqual(NflTeam.DallasCowboys, playerTwoTest.Team);
            Assert.AreEqual(Position.WideReceiver, playerTwoTest.Position);
            refPlayerRepo.Verify();
        }

        [TestMethod]
        public async Task GetAllUserPlayers_UpdatesIdINDatabaseOnSync()
        {
            // Arrange
            var connectorOne = new Mock<IConnector>();
            var connectorTwo = new Mock<IConnector>();
            var refPlayerRepo = new ReferencePlayerRepository(_context);

            var refPlayerTwo = new ReferencePlayer()
            {
                Name = "Michael Gallup",
                Position = (int)Position.WideReceiver,
                Team = (int)NflTeam.DallasCowboys,
            };

            _context.Add(refPlayerTwo);
            _context.SaveChanges();

            var playerOne = new Player()
            {
                Name = "Dak Prescott",
                Team = NflTeam.DallasCowboys,
                Position = Position.QuarterBack,
                Service = new List<ServiceOption> { ServiceOption.MyFantasyLeague }
            };

            var playerTwo = new Player()
            {
                Name = "Michael Gallup",
                Position = Position.None,
                Team = NflTeam.FreeAgent,
                Service = new List<ServiceOption> { ServiceOption.ESPN },
                ServiceIDs = new Dictionary<ServiceOption, int>() { { ServiceOption.ESPN, 12345 } }
            };

            connectorOne.Setup(c => c.GetActivePlayersForUser())
                .ReturnsAsync(new List<Player>() { playerOne });

            connectorTwo.Setup(c => c.GetActivePlayersForUser())
                .ReturnsAsync(new List<Player>() { playerTwo });

            var playerService = new PlayerService(
                new List<IConnector>() { connectorOne.Object, connectorTwo.Object },
                refPlayerRepo
            );

            // Act
            var result = (await playerService.GetAllUserPlayers())
                .ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result
                .Select(p => p.Name)
                .Contains("Dak Prescott"));
            Assert.IsTrue(result
                .Select(p => p.Name)
                .Contains("Michael Gallup"));

            var playerTwoTest = result
                .Where(p => p.Name.Equals("Michael Gallup"))
                .First();

            Assert.AreEqual(NflTeam.DallasCowboys, playerTwoTest.Team);
            Assert.AreEqual(Position.WideReceiver, playerTwoTest.Position);

            var updatedRefPlayer = _context.ReferencePlayers
                .Where(p => p.Name.Equals("Michael Gallup"))
                .FirstOrDefault();

            Assert.AreEqual(12345, updatedRefPlayer.EspnPlayerId);
        }
    }
}
