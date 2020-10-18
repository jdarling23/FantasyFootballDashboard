using System.Collections.Generic;
using System.Linq;
using FantasyFootballDashboard.APIConnector.CBS.Models;
using FantasyFootballDashboard.APIConnector.Exceptions;
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
		private readonly string _leagueName;
		private readonly CbsToken _token;
		private readonly RestClient _client;

		public CbsConnector(string leagueName, string userName, string password)
		{
			_client = new RestClient("https://api.cbssports.com/");
			_leagueName = leagueName;

			_token = GetToken(userName, password);
		}

		/// <summary>
		/// Gets an access token to connect to the CBS Sports API
		/// </summary>
		/// <param name="userName">CBS Sports username (email)</param>
		/// <param name="password">CBS Sports password</param>
		/// <returns>Access token to CBS Sports API</returns>
		private CbsToken GetToken(string userName, string password)
		{
			var request = new RestRequest("general/oauth/test/access_token");
			request.AddParameter("user_id", userName, ParameterType.QueryString);
			request.AddParameter("league_id", _leagueName, ParameterType.QueryString);
			request.AddParameter("sport", "football", ParameterType.QueryString);
			request.AddParameter("response_format", "json", ParameterType.QueryString);

			var response = _client.Post(request);

			var parsedResponse = JsonConvert.DeserializeObject<CbsToken>(response.Content);

			return parsedResponse;
		}

		/// <summary>
		/// Gets the players currently playing for a user. Their team is identified using the logged in CBS token
		/// </summary>
		/// <returns>List of players</returns>
		private List<CbsPlayer> GetActivePlayersForUser()
		{
			var request = new RestRequest("fantasy/league/scoring/live");
			request.AddParameter("version", "3.0", ParameterType.QueryString);
			request.AddParameter("response_format", "json", ParameterType.QueryString);

			_client.AddDefaultHeader("Authorization", $"Bearer {_token}");

			var response = _client.Post(request);

			var parsedResponse = JsonConvert.DeserializeObject<CbsScoringPayload>(response.Content);

			var teamId = parsedResponse?.Body?.LiveScoring?.TeamId;

			if (string.IsNullOrEmpty(teamId))
			{
				throw new TeamNotFoundException("Could not find team for current user.");
			}

			var players = parsedResponse?.Body?.LiveScoring?.Teams?
				.Where(t => t.Id.Equals(teamId))
				.SelectMany(t => t.Players)
				.ToList();

			if (players == null || !players.Any())
			{
				throw new PlayersNotFoundException($"No players found for ID {teamId}");
			}

			return players;
		}
	}
}