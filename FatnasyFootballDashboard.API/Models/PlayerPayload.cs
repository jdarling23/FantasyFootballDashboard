using FantasyFootballDashboard.Models;
using System.Collections.Generic;

namespace FatnasyFootballDashboard.API.Models
{
    /// <summary>
    /// Object containing player data; returned by the API
    /// </summary>
    public class PlayerPayload
    {
        /// <summary>
        /// Create payoad by providing a list of players
        /// </summary>
        /// <param name="players">List of players</param>
        public PlayerPayload(List<Player> players)
        {
            Players = players;
        }

        /// <summary>
        /// Players contained within the payload
        /// </summary>
        public List<Player> Players { get; set; }
    }
}
