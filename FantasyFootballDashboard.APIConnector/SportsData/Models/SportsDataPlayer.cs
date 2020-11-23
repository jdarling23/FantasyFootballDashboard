using FantasyFootballDashboard.APIConnector.Interfaces;
using Newtonsoft.Json;

namespace FantasyFootballDashboard.APIConnector.SportsData.Models
{
    /// <summary>
    /// Object mapping in properties for playes form the SportsData.io API
    /// </summary>
    public class SportsDataPlayer : ReferencePlayerBase
    {
        [JsonProperty("PlayerID")]
        public override int PlayerId { get; set; }

        [JsonProperty("Name")]
        public override string Name { get; set; }

        [JsonProperty("Team")]
        public override string Team { get; set; }

        [JsonProperty("FantasyPosition")]
        public override string Position { get; set; }

        [JsonProperty("PositionCategory")]
        public override string PositionCategory { get; set; }

        [JsonProperty("PhotoUrl")]
        public override string PhotoUrl { get; set; }

        [JsonProperty("Status")]
        public override string Status { get; set; }

        [JsonProperty("Number")]
        public override int? JerseyNumber { get; set; } = 0;

        [JsonProperty("Experience")]
        public override int? YearsInLeague { get; set; } = 0;

        [JsonProperty("College")]
        public override string College { get; set; }

        [JsonProperty("AverageDraftPosition")]
        public override double? AverageDraftPosition { get; set; } = 0;

        [JsonProperty("ByeWeek")]
        public override int? ByeWeek { get; set; } = 0;
    }
}
