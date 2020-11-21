using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.SportsData.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using FantasyFootballDashboard.Models.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.APIConnector.SportsData
{
    /// <summary>
    /// Connector object to SportsData.io API.
    /// Read only API bring back needed data for filing player gaps.
    /// </summary>
    public class SportsDataConnector : IConnector
    {
        private readonly string _apiKey;
        private readonly RestClient _client;

        public SportsDataConnector(string apiKey)
        {
            _client = new RestClient("https://api.sportsdata.io/v3/");
            _apiKey = apiKey;
        }

        /// <summary>
        /// Is not implemented, this cnnector provides player reference data, not user player data.
        /// </summary>
        /// <returns>Not implemented exception</returns>
        public Task<List<Player>> GetActivePlayers()
        {
            throw new NotImplementedException("This endpoint does not need to return players; it is a not a Fantasy Football Service, but a ref data service.");
        }

        /// <summary>
        /// Returns all players frrom SportsData.io
        /// </summary>
        /// <returns>List of SportsDataPlayers</returns>
        public async Task<List<SportsDataPlayer>> GetRefPlayerData()
        {
            var request = new RestRequest("nfl/scores/json/Players");
            request.AddParameter("key", _apiKey, ParameterType.QueryString);

            var response = await _client.ExecuteAsync(request, Method.GET);

            try
            {
                var parsedRepsonse = JsonConvert.DeserializeObject<List<SportsDataPlayer>>(response.Content);
                return parsedRepsonse;
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
