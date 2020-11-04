using FantasyFootballDashboard.Models;
using System.Collections.Generic;

namespace FantasyFootballDashboard.APIConnector.Interface
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
	}

    public class DefaultConnector : IConnector
    {
        public List<Player> GetActivePlayersForUser()
        {
            return new List<Player>();
        }
    }
}