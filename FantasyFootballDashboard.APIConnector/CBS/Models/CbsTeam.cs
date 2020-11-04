using System.Collections.Generic;
using Newtonsoft.Json;

namespace FantasyFootballDashboard.APIConnector.CBS.Models
{
	public class CbsTeam
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("players")]
		public List<CbsPlayer> Players { get; set; }
	}
}