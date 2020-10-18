using Newtonsoft.Json;

namespace FantasyFootballDashboard.APIConnector.CBS.Models
{
	public class CbsPlayer
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("fullname")]
		public string FullName { get; set; }

		[JsonProperty("position")]
		public string Position { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }
	}
}