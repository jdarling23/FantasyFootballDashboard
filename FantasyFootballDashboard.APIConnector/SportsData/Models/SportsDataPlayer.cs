using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyFootballDashboard.APIConnector.SportsData.Models
{
    /// <summary>
    /// Object mapping in properties for playes form the SportsData.io API
    /// </summary>
    public class SportsDataPlayer
    {
        [JsonProperty("PlayerID")]
        public int PlayerId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Team")]
        public string Team { get; set; }

        [JsonProperty("FantasyPosition")]
        public string Position { get; set; }

        [JsonProperty("PositionCategory")]
        public string PositionCategory { get; set; }

        [JsonProperty("PhotoUrl")]
        public string Photorl { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Number")]
        public int? JerseyNumber { get; set; } = 0;

        [JsonProperty("Experience")]
        public int? YearsInLeague { get; set; } = 0;

        [JsonProperty("College")]
        public string College { get; set; }

        [JsonProperty("AverageDraftPosition")]
        public double? AverageDraftPosition { get; set; } = 0;

        [JsonProperty("ByeWeek")]
        public int? ByeWeek { get; set; } = 0;
    }
}
