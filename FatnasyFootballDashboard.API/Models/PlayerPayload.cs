using FantasyFootballDashboard.Models;
using System.Collections.Generic;

namespace FatnasyFootballDashboard.API.Models
{
    public class PlayerPayload
    {
        public PlayerPayload(List<Player> players)
        {
            Players = players;
        }

        public List<Player> Players { get; set; }
    }
}
