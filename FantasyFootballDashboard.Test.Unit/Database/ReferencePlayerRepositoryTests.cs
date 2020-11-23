using FantasyFootbalDashboard.DBConnector;
using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootbalDashboard.DBConnector.Repositories;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FantasyFootballDashboard.Test.Unit.Database
{
    [TestClass]
    public class ReferencePlayerRepositoryTests
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
        public void GetReferencePlayer_CanFindByName()
        {
            // Arrange
            var testPlayer = new Player()
            {
                Name = "Michael Gallup",
                Position = Position.None,
                Team = NflTeam.FreeAgent,
                Service = new List<ServiceOption> { ServiceOption.ESPN },
                ServiceIDs = new Dictionary<ServiceOption, int>() { { ServiceOption.ESPN, 12345 } }
            };

            var referencePlayer = new ReferencePlayer()
            {
                Name = "Michael Gallup",
                Position = (int)Position.WideReceiver,
                Team = (int)NflTeam.DallasCowboys,
            };

            _context.Add(referencePlayer);
            _context.SaveChanges();

            var repo = new ReferencePlayerRepository(_context);

            // Act
            var result = repo.GetReferencePlayer(testPlayer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(referencePlayer.Name, result.Name);
            Assert.AreEqual(referencePlayer.Position, result.Position);
            Assert.AreEqual(referencePlayer.Team, result.Team);
        }

        [TestMethod]
        public void GetReferencePlayer_CanFindByServiceId()
        {
            // Arrange
            var testPlayer = new Player()
            {
                Name = "Michael Gallup",
                Position = Position.None,
                Team = NflTeam.FreeAgent,
                Service = new List<ServiceOption> { ServiceOption.ESPN },
                ServiceIDs = new Dictionary<ServiceOption, int>() { { ServiceOption.ESPN, 12345 } }
            };

            var referencePlayer = new ReferencePlayer()
            {
                Name = "Michael Gallllllup", //Testing what happens if we are mispelled but have the ID
                Position = (int)Position.WideReceiver,
                Team = (int)NflTeam.DallasCowboys,
                EspnPlayerId = 12345
            };

            _context.Add(referencePlayer);
            _context.SaveChanges();

            var repo = new ReferencePlayerRepository(_context);

            // Act
            var result = repo.GetReferencePlayer(testPlayer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(referencePlayer.Name, result.Name);
            Assert.AreEqual(referencePlayer.Position, result.Position);
            Assert.AreEqual(referencePlayer.Team, result.Team);
        }

    }
}
