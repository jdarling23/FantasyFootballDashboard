using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Interface;
using RestSharp;
using System;
using System.Collections.Generic;

namespace FantasyFootballDashboard.APIConnector.MFL
{
    /// <summary>
    /// Connects to MyFantasyLeague API to interact with their data
    /// </summary>
    public class MflConnector : IConnector
    {
        private readonly Dictionary<string, string> _cookies;
        private readonly RestClient _client;

        public MflConnector(string year, string username, string password)
        {
            _client = new RestClient($"https://api.myfantasyleague.com/{year}/");
            _cookies = GetMflCookies(username, password);
        }

        public List<Player> GetActivePlayersForUser()
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> GetMflCookies(string username, string password)
        {
            var request = new RestRequest($"login");
            request.AddParameter("USERNAME", username, ParameterType.QueryString);
            request.AddParameter("PASSWORD", password, ParameterType.QueryString);
            request.AddParameter("XML", "1", ParameterType.QueryString);

            var response = _client.Get(request);

            var parsedCookies = new Dictionary<string, string>();

            foreach (var cookie in response.Cookies)
            {
                parsedCookies.Add(cookie.Name, cookie.Value);
            }

            return parsedCookies;
        }
    }
}
