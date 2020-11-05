using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyFootballDashboard.APIConnector.MFL.Models
{
    public class MflLeague
    {
        [JsonProperty("league_id")]
        public string LeagueId { get; set; }

        [JsonProperty("name")]
        public string LeagueName { get; set; }

        [JsonProperty("franchise_id")]
        public string FranchiseId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class MflLeagueRequestPayload
    {
        [JsonProperty("leagues")]
        public LeagueContainer LeagueContainer { get; set; }
    }

    public class LeagueContainer
    {
        [JsonProperty("league")]
        public List<MflLeague> Leagues { get; set; }
    }
}
