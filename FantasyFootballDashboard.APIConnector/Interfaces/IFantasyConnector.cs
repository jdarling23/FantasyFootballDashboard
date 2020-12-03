using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.APIConnector.Interfaces
{
    /// <summary>
    /// Interface for an API connector object for Fantasy Football service
    /// </summary>
    public interface IFantasyConnector
	{
		/// <summary>
		/// Gets the players currently playing for a user from a service
		/// </summary>
		/// <returns>List of players</returns>
		Task<List<Player>> GetActivePlayers();

        /// <summary>
        /// Identifies an IConnector by service option
        /// </summary>
        /// <returns></returns>
        ServiceOption GetServiceOption();
	}

    public class DefaultFantasyConnector : IFantasyConnector
    {
        public Task<List<Player>> GetActivePlayers()
        {
            return Task.FromResult(new List<Player>());
        }

        public ServiceOption GetServiceOption()
        {
            return ServiceOption.Default;
        }
    }
}