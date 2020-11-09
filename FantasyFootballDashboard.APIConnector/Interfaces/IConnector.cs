using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using System.Collections.Generic;

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
		List<Player> GetActivePlayersForUser();

        /// <summary>
        /// Identifies an IConnector by service option
        /// </summary>
        /// <returns></returns>
        ServiceOptions GetServiceOption();
	}

    public class DefaultConnector : IConnector
    {
        public List<Player> GetActivePlayersForUser()
        {
            return new List<Player>();
        }

        public ServiceOptions GetServiceOption()
        {
            return ServiceOptions.Default;
        }
    }
}