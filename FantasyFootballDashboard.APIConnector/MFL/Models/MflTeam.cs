using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyFootballDashboard.APIConnector.MFL.Models
{
    public class MflTeam
    {
        [JsonProperty("franchise")]
        public Franchise Franchise { get; set; }
    }

    public class MflRosterRequestPaylod
    {
        [JsonProperty("rosters")]
        public MflTeam Team { get; set; }
    }

    public class Franchise
    {
        [JsonProperty("week")]
        public string NflWeek { get; set; }

        [JsonProperty("player")]
        public List<RosterPlayer> RosterPlayers { get; set; }
    }

    public class RosterPlayer
    {
        [JsonProperty("status")]
        public string PlayerStatus { get; set; }

        [JsonProperty("id")]
        public string PlayerId { get; set; }
    }
}
