using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.APIConnector.Interfaces
{
    /// <summary>
    /// Interface for an API connection
    /// </summary>
    public interface IConnector
	{
		/// <summary>
		/// Gets the players currently playing for a user
		/// </summary>
		/// <returns>List of players</returns>
		Task<List<Player>> GetActivePlayersForUser();

        /// <summary>
        /// Identifies an IConnector by service option
        /// </summary>
        /// <returns></returns>
        ServiceOptions GetServiceOption();
	}

    public class DefaultConnector : IConnector
    {
        public Task<List<Player>> GetActivePlayersForUser()
        {
            return Task.FromResult(new List<Player>());
        }

        public ServiceOptions GetServiceOption()
        {
            return ServiceOptions.Default;
        }
    }
}