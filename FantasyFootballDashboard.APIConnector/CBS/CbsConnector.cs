using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FantasyFootballDashboard.APIConnector.CBS.Models;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using FantasyFootballDashboard.Models.Exceptions;
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
		private readonly CbsPlayerMapper _playerMapper;
		private readonly CbsToken _token;
		private readonly RestClient _client;

		public CbsConnector(string leagueName, string userName)
		{
			_client = new RestClient("https://api.cbssports.com/");
			_leagueName = leagueName;

			_token = GetToken(userName);

			_playerMapper = new CbsPlayerMapper();
		}

		/// <summary>
		/// Gets the players currently playing for a user. Their team is identified using the logged in CBS token
		/// </summary>
		/// <returns>List of players</returns>
		public async Task<List<Player>> GetActivePlayers()
		{
			var request = new RestRequest("fantasy/league/scoring/live");
			request.AddParameter("version", "3.0", ParameterType.QueryString);
			request.AddParameter("response_format", "json", ParameterType.QueryString);

			_client.AddDefaultHeader("Authorization", $"Bearer {_token.Body.AccessToken}");

			var response = await _client.ExecuteAsync(request, Method.GET);

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

			var mappedPlayers = players.Select(p => _playerMapper.ConvertPlayer(p));

			return mappedPlayers.ToList();
		}

		/// <summary>
		/// Returns enum for this connector
		/// </summary>
		/// <returns>CBS Service Option</returns>
		public ServiceOption GetServiceOption()
        {
			return ServiceOption.CBS;
        }

        private CbsToken GetToken(string userName)
		{
			var request = new RestRequest("general/oauth/test/access_token");
			request.AddParameter("user_id", userName, ParameterType.QueryStringWithoutEncode);
			request.AddParameter("league_id", _leagueName, ParameterType.QueryString);
			request.AddParameter("sport", "football", ParameterType.QueryString);
			request.AddParameter("response_format", "json", ParameterType.QueryString);

            try
            {
                var response = _client.Get(request);

                var parsedResponse = JsonConvert.DeserializeObject<CbsToken>(response.Content);

                return parsedResponse;
            }
            catch (Exception ex)
            {
				throw new ServiceLoginException($"Could not generate token for CBS: {ex.Message}");
            }
		}
	}
}