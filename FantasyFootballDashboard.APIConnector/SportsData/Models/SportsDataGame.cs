using Newtonsoft.Json;
using System;

namespace FantasyFootballDashboard.APIConnector.SportsData.Models
{
    /// <summary>
    /// Ojbect mapping data for NFL games from the SportsData.io API
    /// </summary>
    public class SportsDataGame
    {
        [JsonProperty("GameKey")]
        public string Key { get; set; }

        [JsonProperty("Season")]
        public int Season { get; set; }

        [JsonProperty("AwayTeam")]
        public string AwayTeam { get; set; }

        [JsonProperty("HomeTeam")]
        public string HomeTeam { get; set; }

        [JsonProperty("Channel")]
        public string Channel { get; set; }

        [JsonProperty("DateTime")]
        public DateTime? GameDateTime { get; set; }
    }
}
