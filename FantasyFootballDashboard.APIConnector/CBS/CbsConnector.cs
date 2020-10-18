using FantasyFootballDashboard.APIConnector.Interface;
using FantasyFootballDashboard.APIConnector.Models;
using Newtonsoft.Json;
using RestSharp;

namespace FantasyFootballDashboard.APIConnector.CBS
{
	/// <summary>
	/// Connects to CBS Sports API to interact with their data
	/// </summary>
	public class CbsConnector : IConnector
	{
		private string _leagueName;
		private readonly RestClient client;

		public CbsConnector(string leagueName)
		{
			client = new RestClient("https://api.cbssports.com/");
			_leagueName = leagueName;
		}

		/// <summary>
		/// Get's an access token to connect to the CBS Sports API
		/// </summary>
		/// <param name="userName">CBS Sports username (email)</param>
		/// <param name="password">CBS Sports password</param>
		/// <param name="targetUrl">Endpoint address for CBS authentication, beginning of URL already provided</param>
		/// <returns>Access token to CBS Sports API</returns>
		public AccessToken GetToken(string userName, string password, string targetUrl)
		{
			var request = new RestRequest(targetUrl);
			request.AddParameter("user_id", userName, ParameterType.QueryString);
			request.AddParameter("league_id", _leagueName, ParameterType.QueryString);
			request.AddParameter("sport", "football", ParameterType.QueryString);
			request.AddParameter("response_format", "json", ParameterType.QueryString);

			var response = client.Post(request);

			var parsedResponse = JsonConvert.DeserializeObject<CbsToken>(response.Content);

			return parsedResponse;
		}
	}
}