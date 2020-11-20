using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyFootballDashboard.APIConnector.MFL.Models
{
    public class MflPlayer
    {
        [JsonProperty("id")]
        public string PlayerId { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        private string _name;

        [JsonProperty("name")]
        public string FullName 
        { 
            get => _name;
            set => _name = 
                (value.Substring(value.IndexOf(" "), value.Length - value.IndexOf(" ")) + 
                " " + 
                value.Substring(0, value.IndexOf(","))).Trim();
        }

        [JsonProperty("team")]
        public string NflTeam { get; set; }
    }

    public class MflPlayerRequestPayload
    {
        [JsonProperty("players")]
        public PlayersContainer PlayersContainer { get; set; }
    }

    public class PlayersContainer
    {
        [JsonProperty("player")]
        public List<MflPlayer> Players { get; set; }
    }
}
