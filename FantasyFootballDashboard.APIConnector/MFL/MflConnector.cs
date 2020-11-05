using FantasyFootballDashboard.APIConnector.MFL.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Exceptions;
using FantasyFootballDashboard.Models.Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyFootballDashboard.APIConnector.MFL
{
    /// <summary>
    /// Connects to MyFantasyLeague API to interact with their data
    /// </summary>
    public class MflConnector : IConnector
    {
        private readonly Dictionary<string, string> _cookies;
        private readonly RestClient _client;

        private List<MflLeague> _leagues;

        public MflConnector(string year, string username, string password)
        {
            _client = new RestClient($"https://api.myfantasyleague.com/{year}/");
            _cookies = GetMflCookies(username, password);

            _leagues = GetUserLeagues(year);
        }

        public List<Player> GetActivePlayersForUser()
        {
            throw new NotImplementedException();
        }

        private List<MflLeague> GetUserLeagues(string year)
        {
            var request = new RestRequest("export");
            request.AddParameter("TYPE", "myleagues", ParameterType.QueryString);
            request.AddParameter("YEAR", year, ParameterType.QueryString);
            request.AddParameter("JSON", "1", ParameterType.QueryString);

            foreach(var cookie in _cookies)
            {
                request.AddCookie(cookie.Key, cookie.Value);
            }

            var response = _client.Get(request);

            var parsedLeaguePayload = JsonConvert.DeserializeObject<MflLeagueRequestPayload>(response.Content);

            if(parsedLeaguePayload?.LeagueContainer?.Leagues == null)
            {
                throw new ServiceLoginException("Could not identify leagues for user in My Fantasy League");
            }

            return parsedLeaguePayload.LeagueContainer.Leagues;
        }

        private Dictionary<string, string> GetMflCookies(string username, string password)
        {
            var request = new RestRequest("login");
            request.AddParameter("USERNAME", username, ParameterType.QueryString);
            request.AddParameter("PASSWORD", password, ParameterType.QueryString);
            request.AddParameter("XML", "1", ParameterType.QueryString);

            var response = _client.Get(request);

            var parsedCookies = new Dictionary<string, string>();

            foreach (var cookie in response.Cookies)
            {
                parsedCookies.Add(cookie.Name, cookie.Value);
            }

            if (!parsedCookies.Any())
            {
                throw new ServiceLoginException("Failed to generate cookies for My Fantasy League.");
            }

            return parsedCookies;
        }
    }
}
