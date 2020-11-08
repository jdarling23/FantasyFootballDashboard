using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyFootballDashboard.APIConnector.ESPN.Models
{
    public class EspnTeam
    {
        [JsonProperty("id")]
        public int TeamId { get; set; }

        [JsonProperty("roster")]
        public EspnRoster Roster { get; set; }
    }

    public class EspnRosterRequestPayload
    {
        [JsonProperty("teams")]
        public List<EspnTeam> Team { get; set; }
    }

    public class EspnRoster
    {
        [JsonProperty("entries")]
        public List<EspnPlayerEntry> PlayerEntries { get; set; }
    }
}
