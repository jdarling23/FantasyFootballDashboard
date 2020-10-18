using FantasyFootballDashboard.APIConnector.Models;
using Newtonsoft.Json;

namespace FantasyFootballDashboard.APIConnector.CBS.Models
{
	public class CbsToken: AccessToken
	{
		[JsonProperty("statusCode")]
		public int StatusCode { get; set; }

		[JsonProperty("body")]
		public TokenBody Body { get; set; }
	}

	public class TokenBody
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
	}
}