using FantasyFootbalDashboard.DBConnector;
using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootbalDashboard.DBConnector.Repositories;
using FantasyFootballDashboard.Models.Enums;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyFootballDashboard.Test.Unit.Database
{
    [TestClass]
    public class ReferenceGameRepositoryTests
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
        public void GetGamesByTeams_GetsAllGamesForATeam()
        {
            // Arrange
            var gameOne = new ReferenceGame()
            {
                GameId = 1,
                SeasonYear = 2020,
                AwayTeam = (int)NflTeam.DallasCowboys,
                HomeTeam = (int)NflTeam.NewEnglandPatriots,
                Week = 1,
                NetworkChannel = "CBS",
                GameDateTime = new DateTime(2020, 9, 1)
            };

            var gameTwo = new ReferenceGame()
            {
                GameId = 2,
                SeasonYear = 2020,
                AwayTeam = (int)NflTeam.PhilidelphiaEagles,
                HomeTeam = (int)NflTeam.DallasCowboys,
                Week = 1,
                NetworkChannel = "NBC",
                GameDateTime = new DateTime(2020, 9, 8)
            };

            var gameThree = new ReferenceGame()
            {
                GameId = 3,
                SeasonYear = 2020,
                AwayTeam = (int)NflTeam.LosAngelesRams,
                HomeTeam = (int)NflTeam.ArizonaCardinals,
                Week = 1,
                NetworkChannel = "NBC",
                GameDateTime = new DateTime(2020, 9, 8)
            };

            var gameToExclude = new ReferenceGame()
            {
                GameId = 4,
                SeasonYear = 2020,
                AwayTeam = (int)NflTeam.PhilidelphiaEagles,
                HomeTeam = (int)NflTeam.NewYorkGiants,
                Week = 1,
                NetworkChannel = "FOX",
                GameDateTime = new DateTime(2020, 9, 15)
            };

            _context.Add(gameOne);
            _context.Add(gameTwo);
            _context.Add(gameThree);
            _context.Add(gameToExclude);
            _context.SaveChanges();

            var targetTeams = new List<NflTeam>() 
            { 
                NflTeam.DallasCowboys, 
                NflTeam.ArizonaCardinals 
            };

            var repo = new ReferenceGameRepository(_context);

            // Act
            var result = repo.GetGamesByTeams(targetTeams);

            // Asserrt
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result
                .Where(g => g.HomeTeam == (int)NflTeam.DallasCowboys)
                .ToList()
                .Count);
            Assert.AreEqual(1, result
                .Where(g => g.AwayTeam == (int)NflTeam.DallasCowboys)
                .ToList()
                .Count);
            Assert.AreEqual(1, result
                .Where(g => g.HomeTeam == (int)NflTeam.ArizonaCardinals)
                .ToList()
                .Count);
        }
    }
}
