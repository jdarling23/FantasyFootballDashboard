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
		/// Gets the players currently playing for a user from a service
		/// </summary>
		/// <returns>List of players</returns>
		Task<List<Player>> GetActivePlayers();

        /// <summary>
        /// Gets reference player data from a service
        /// </summary>
        /// <returns></returns>
        Task<List<ReferencePlayerBase>> GetReferencePlayers();

        /// <summary>
        /// Identifies an IConnector by service option
        /// </summary>
        /// <returns></returns>
        ServiceOption GetServiceOption();
	}

    public class DefaultConnector : IConnector
    {
        public Task<List<Player>> GetActivePlayers()
        {
            return Task.FromResult(new List<Player>());
        }

        public Task<List<ReferencePlayerBase>> GetReferencePlayers()
        {
            return Task.FromResult(new List<ReferencePlayerBase>());
        }

        public ServiceOption GetServiceOption()
        {
            return ServiceOption.Default;
        }
    }
}