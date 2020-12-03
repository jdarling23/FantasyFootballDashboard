using FantasyFootbalDashboard.DBConnector.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootbalDashboard.DBConnector
{
    /// <summary>
    /// Database context for working with the reference database
    /// </summary>
    public class FantasyFootballDashboardContext : DbContext
    {
        public DbSet<ReferenceGame> ReferenceGames { get; set; }

        public DbSet<ReferencePlayer> ReferencePlayers { get; set; }

        public FantasyFootballDashboardContext(DbContextOptions<FantasyFootballDashboardContext> options)
        : base(options)
        {
        }

    }
}
