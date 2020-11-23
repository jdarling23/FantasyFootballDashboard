using FantasyFootbalDashboard.DBConnector;
using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootbalDashboard.DBConnector.Repositories;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.SportsData;
using FantasyFootballDashboard.APIConnector.SportsData.Models;
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
    public class DataServiceTests
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
        public async Task ImportSportsDataReferenceData_ImportsAndUpdatesActiveOffensivePlayersData()
        {
            // Arrange
            var existingRefPlayer = new ReferencePlayer()
            {
                Name = "Michael Gallup",
                Position = (int)Position.WideReceiver,
                Team = (int)NflTeam.DallasCowboys,
                SportsDataIoPlayerId = 1,
                College = "Colorado State",
            };

            _context.Add(existingRefPlayer);
            _context.SaveChanges();

            // Should update missing fields in database
            var exisitingSportsDataPlayer = new SportsDataPlayer()
            {
                PlayerId = 1,
                Name = "Michael Gallup",
                Team = "DAL",
                JerseyNumber = 13,
                Status = "Active",
                Position = "WR",
                PositionCategory = "OFF",
                YearsInLeague = 4,
                AverageDraftPosition = 81.2,
                College = "Colorado State",
                ByeWeek = 10
            };

            // Should come in to the database
            var newSportsDataPlayer = new SportsDataPlayer()
            {
                PlayerId = 2,
                Name = "James Washington",
                Team = "PIT",
                JerseyNumber = 13,
                Status = "Active",
                Position = "WR",
                PositionCategory = "OFF",
                YearsInLeague = 4,
                AverageDraftPosition = 203,
                College = "Oklahoma State",
                ByeWeek = 4
            };

            // Should come into database, but marked as inactive
            var injuredSportsDataPlayer = new SportsDataPlayer()
            {
                PlayerId = 3,
                Name = "Saquon Barkley",
                Team = "NYG",
                JerseyNumber = 26,
                Status = "Injured Reserve",
                Position = "RB",
                PositionCategory = "OFF",
                YearsInLeague = 4,
                AverageDraftPosition = 3.3,
                College = "Penn State",
                ByeWeek = 11
            };

            // Should not come into the database
            var defensiveSportsDataPlayer = new SportsDataPlayer()
            {
                PlayerId = 4,
                Name = "Emmanuel Ogbah",
                Team = "MIA",
                JerseyNumber = 91,
                Status = "Active",
                Position = "DL",
                PositionCategory = "DEF",
                YearsInLeague = 7,
                AverageDraftPosition = 1113.7,
                College = "Oklahoma State",
                ByeWeek = 7
            };

            // Should not come in to the database
            var inactiveSportsDataPlayer = new SportsDataPlayer()
            {
                PlayerId = 5,
                Name = "Sam Bradford",
                Team = null,
                JerseyNumber = 9,
                Status = "Inactive",
                Position = "QB",
                PositionCategory = "OFF",
                YearsInLeague = 11,
                AverageDraftPosition = 0,
                College = "Oklahoma",
                ByeWeek = 0
            };

            var sportsDataPlayerResults = new List<ReferencePlayerBase>()
            {
                exisitingSportsDataPlayer,
                newSportsDataPlayer,
                injuredSportsDataPlayer,
                defensiveSportsDataPlayer,
                inactiveSportsDataPlayer
            };

            var sportsDataConnector = new Mock<IConnector>();
            sportsDataConnector.Setup(c => c.GetReferencePlayers())
                .ReturnsAsync(sportsDataPlayerResults);

            var refPlayerRepository = new ReferencePlayerRepository(_context);

            var dataService = new DataService(sportsDataConnector.Object, refPlayerRepository);

            // Act
            var result = await dataService.ImportSportsDataReferenceData();

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(_context.ReferencePlayers.
                Where(p => p.Name.Equals("Sam Bradford") || p.Name.Equals("Emmanuel Ogbah"))
                .Any());

            var updatedPlayer = _context.ReferencePlayers
                .Where(p => p.Name.Equals("Michael Gallup"))
                .First();
            Assert.AreEqual((int)NflTeam.DallasCowboys, updatedPlayer.Team);
            Assert.AreEqual((int)Position.WideReceiver, updatedPlayer.Position);
            Assert.AreEqual("Active", updatedPlayer.Status);
            Assert.AreEqual(13, updatedPlayer.JerseyNumber);
            Assert.AreEqual(4, updatedPlayer.YearsInLeague);
            Assert.AreEqual(81.2, updatedPlayer.AverageDraftPosition);
            Assert.AreEqual(10, updatedPlayer.ByeWeek);
            Assert.AreEqual("Colorado State", updatedPlayer.College);
            Assert.IsTrue(updatedPlayer.Active);

            var newPlayer = _context.ReferencePlayers
                .Where(p => p.Name.Equals("James Washington"))
                .First();
            Assert.AreEqual((int)NflTeam.PittsburghSteelers, newPlayer.Team);
            Assert.AreEqual((int)Position.WideReceiver, newPlayer.Position);
            Assert.AreEqual("Active", newPlayer.Status);
            Assert.AreEqual(13, newPlayer.JerseyNumber);
            Assert.AreEqual(4, newPlayer.YearsInLeague);
            Assert.AreEqual(203, newPlayer.AverageDraftPosition);
            Assert.AreEqual(4, newPlayer.ByeWeek);
            Assert.AreEqual("Oklahoma State", newPlayer.College);
            Assert.IsTrue(newPlayer.Active);

            var injuredPlayer = _context.ReferencePlayers
                .Where(p => p.Name.Equals("Saquon Barkley"))
                .First();
            Assert.AreEqual((int)NflTeam.NewYorkGiants, injuredPlayer.Team);
            Assert.AreEqual((int)Position.RunningBack, injuredPlayer.Position);
            Assert.AreEqual("Injured Reserve", injuredPlayer.Status);
            Assert.AreEqual(26, injuredPlayer.JerseyNumber);
            Assert.AreEqual(4, injuredPlayer.YearsInLeague);
            Assert.AreEqual(3.3, injuredPlayer.AverageDraftPosition);
            Assert.AreEqual(11, injuredPlayer.ByeWeek);
            Assert.AreEqual("Penn State", injuredPlayer.College);
            Assert.IsFalse(injuredPlayer.Active);
        }
    }
}
