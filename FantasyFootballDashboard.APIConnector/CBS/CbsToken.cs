using FantasyFootballDashboard.APIConnector.Models;

namespace FantasyFootballDashboard.APIConnector.CBS
{
	public class CbsToken: AccessToken
	{
		public string Uri { get; set; }

		public string StatusMessage { get; set; }

		public string UriAlias { get; set; }

		public int StatusCode { get; set; }

		protected class Body ()
		{
			public string AccessToken { get; set; }
		}
	}
}