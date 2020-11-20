using FantasyFootbalDashboard.DBConnector.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootbalDashboard.DBConnector
{
    /// <summary>
    /// Database context for working with the reference database
    /// </summary>
    public class FantasyFootballDashboardContext : DbContext
    {
        private string _connectionString;

        public DbSet<ReferencePlayer> ReferencePlayers { get; set; }

        public FantasyFootballDashboardContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(_connectionString);
    }
}
