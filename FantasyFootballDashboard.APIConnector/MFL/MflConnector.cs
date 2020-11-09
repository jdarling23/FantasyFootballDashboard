using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.MFL.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;
using FantasyFootballDashboard.Models.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.APIConnector.MFL
{
    /// <summary>
    /// Connects to MyFantasyLeague API to interact with their data
    /// </summary>
    public class MflConnector : IConnector
    {
        private readonly Dictionary<string, string> _cookies;
        private readonly RestClient _client;
        private readonly string _year;

        private List<MflLeague> _leagues;

        public MflConnector(string year, string username, string password)
        {
            _client = new RestClient($"https://api.myfantasyleague.com/{year}/");
            _year = year;

            _cookies = GetMflCookies(username, password);

            _leagues = GetUserLeagues();
        }

        public async Task<List<Player>> GetActivePlayersForUser()
        {
            var playersToReturn = new List<Player>();

            foreach(var league in _leagues)
            {
                var playerClient = new RestClient();
                playerClient.BaseUrl = new Uri(league.Url);
                var request = new RestRequest($"{_year}/export");
                request.AddParameter("TYPE", "rosters", ParameterType.QueryString);
                request.AddParameter("L", league.LeagueId, ParameterType.QueryString);
                request.AddParameter("FRANCHISE", league.FranchiseId, ParameterType.QueryString);
                request.AddParameter("JSON", "1", ParameterType.QueryString);

                foreach (var cookie in _cookies)
                {
                    request.AddCookie(cookie.Key, cookie.Value);
                }

                var response = await playerClient.ExecuteAsync(request, Method.GET);

                var parsedLeaguePayload = JsonConvert.DeserializeObject<MflRosterRequestPaylod>(response.Content);

                if (parsedLeaguePayload?.Team?.Franchise?.RosterPlayers == null)
                {
                    continue;
                }

                // The players returned from the Roster request are simply IDs. We need to look up their details in another call
                var playerIds = parsedLeaguePayload.Team.Franchise.RosterPlayers
                        .Select(p => p.PlayerId)
                        .ToList();

                var detailedMflPlayers = await LookupMflPlayersById(playerIds, playerClient);

                var mapper = new MflPlayerMapper();
                var mappedPlayers = detailedMflPlayers
                    .Select(p => mapper.ConvertPlayer(p));

                playersToReturn.AddRange(mappedPlayers);
            }

            if (!playersToReturn.Any())
            {
                throw new PlayersNotFoundException("Could not identify players for user in My Fantasy League; team was empty or not found.");
            }

            return playersToReturn;
        }

        private List<MflLeague> GetUserLeagues()
        {
            var request = new RestRequest("export");
            request.AddParameter("TYPE", "myleagues", ParameterType.QueryString);
            request.AddParameter("YEAR", _year, ParameterType.QueryString);
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

        public ServiceOptions GetServiceOption()
        {
            return ServiceOptions.Mfl;
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

        private async Task<List<MflPlayer>> LookupMflPlayersById(List<string> playerIds, RestClient client)
        {
            var request = new RestRequest($"{_year}/export");
            request.AddParameter("TYPE", "players", ParameterType.QueryString);
            request.AddParameter("L", _leagues[0].LeagueId, ParameterType.QueryString);
            request.AddParameter("PLAYERS", string.Join(",", playerIds), ParameterType.QueryStringWithoutEncode);
            request.AddParameter("JSON", "1", ParameterType.QueryString);

            foreach (var cookie in _cookies)
            {
                request.AddCookie(cookie.Key, cookie.Value);
            }

            var response = await client.ExecuteAsync(request, Method.GET);

            var parsedPlayerPayload = JsonConvert.DeserializeObject<MflPlayerRequestPayload>(response.Content);

            if (parsedPlayerPayload?.PlayersContainer?.Players == null)
            {
                throw new PlayersNotFoundException("Could not look up players in My Fantasy League; none found.");
            }

            return parsedPlayerPayload?.PlayersContainer?.Players;
        }

    }
}
