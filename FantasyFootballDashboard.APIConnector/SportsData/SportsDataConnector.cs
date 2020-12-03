using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.SportsData.Models;
using FantasyFootballDashboard.Models.Enums;
using FantasyFootballDashboard.Models.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.APIConnector.SportsData
{
    /// <summary>
    /// Connector object to SportsData.io API.
    /// Read only API bring back needed data for filing player gaps.
    /// </summary>
    public class SportsDataConnector : IReferenceConnector
    {
        private readonly string _apiKey;
        private readonly RestClient _client;

        public SportsDataConnector(string apiKey)
        {
            _client = new RestClient("https://api.sportsdata.io/v3/");
            _apiKey = apiKey;
        }

        /// <summary>
        /// Gets all NFL games for a given season 
        /// </summary>
        /// <param name="year">Year for desired seasn (e.g. 2020, 2017, etc)</param>
        /// <returns>List of game objects</returns>
        public async Task<List<SportsDataGame>> GetNflGames(string year)
        {
            var request = new RestRequest($"nfl/scores/json/Schedules/{year}");
            request.AddParameter("key", _apiKey, ParameterType.QueryString);

            var response = await _client.ExecuteAsync(request, Method.GET);


            try
            {
                var parsedRepsonse = JsonConvert.DeserializeObject<List<SportsDataGame>>(response.Content);
                return parsedRepsonse.ToList();
            }
            catch (Exception ex)
            {
                throw new GamesNotFoundException($"Could not NFL game data from SportsData.io for {year} season: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns all reference players frrom SportsData.io
        /// </summary>
        /// <returns>List of SportsDataPlayers</returns>
        public async Task<List<ReferencePlayerBase>> GetReferencePlayers()
        {
            var request = new RestRequest("nfl/scores/json/Players");
            request.AddParameter("key", _apiKey, ParameterType.QueryString);

            var response = await _client.ExecuteAsync(request, Method.GET);

            try
            {
                var parsedRepsonse = JsonConvert.DeserializeObject<List<SportsDataPlayer>>(response.Content);
                return parsedRepsonse
                    .Select(pr => pr as ReferencePlayerBase)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new PlayersNotFoundException($"Could not retrieve player data from SportsData.io: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns SportsData service optin
        /// </summary>
        /// <returns>ServiceOption enum</returns>
        public ServiceOption GetServiceOption()
        {
            return ServiceOption.SportsData;
        }
    }

}
