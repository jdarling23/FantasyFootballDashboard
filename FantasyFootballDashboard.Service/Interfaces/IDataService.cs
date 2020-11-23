using System.Threading.Tasks;

namespace FantasyFootballDashboard.Service.Interfaces
{
    /// <summary>
    /// Provides ability to maintain data tables for player reference table.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Saves reference player data from SportsData.io into the database
        /// </summary>
        /// <returns></returns>
        Task<bool> ImportSportsDataReferenceData();
    }
}
