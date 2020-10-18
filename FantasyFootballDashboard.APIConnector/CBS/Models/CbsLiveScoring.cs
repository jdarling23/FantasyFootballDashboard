using System.Collections.Generic;
using Newtonsoft.Json;

namespace FantasyFootballDashboard.APIConnector.CBS.Models
{
	public class CbsScoringPayload
	{
		[JsonProperty("body")]
		public CbsScoringPayloadBody Body { get; set; }
	}

	public class CbsScoringPayloadBody
	{
		[JsonProperty("live_scoring")]
		public CbsLiveScoring LiveScoring { get; set; }
	}

	public class CbsLiveScoring
	{
		[JsonProperty("my_team_id")]
		public string TeamId { get; set; }

		[JsonProperty("period")]
		public string Week { get; set; }

		[JsonProperty("teams")]
		public List<CbsTeam> Teams { get; set; }
	}
}