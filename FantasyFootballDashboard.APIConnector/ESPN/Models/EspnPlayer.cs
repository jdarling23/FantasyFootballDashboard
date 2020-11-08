using Newtonsoft.Json;

namespace FantasyFootballDashboard.APIConnector.ESPN.Models
{
    public class EspnPlayer
    {
        [JsonProperty("id")]
        public int EspnPlayerId { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }
    }

    public class EspnPlayerEntry
    {
        [JsonProperty("playerPoolEntry")]
        public EspnPlayerPoolEntry PlayerPoolEntry { get; set; }
    }

    public class EspnPlayerPoolEntry
    {
        [JsonProperty("player")]
        public EspnPlayer Player { get; set; }
    }
}
