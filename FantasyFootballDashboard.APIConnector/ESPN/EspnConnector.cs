﻿using FantasyFootballDashboard.APIConnector.ESPN.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Exceptions;
using FantasyFootballDashboard.Models.Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyFootballDashboard.APIConnector.ESPN
{
    public class EspnConnector : IConnector
    {
        private readonly RestClient _client;

        private string _teamId;

        public EspnConnector(string year, string leagueId, string teamId)
        {
            _client = new RestClient($"https://fantasy.espn.com/apis/v3/games/ffl/seasons/{year}/segments/0/leagues/{leagueId}");
            _teamId = teamId;
        }

        public List<Player> GetActivePlayersForUser()
        {
            var request = new RestRequest();
            request.AddParameter("view", "mRoster", ParameterType.QueryString);
            request.AddParameter("forTeamId", _teamId, ParameterType.QueryString);

            var response = _client.Get(request);
            var parsedResponse = new EspnRosterRequestPayload();

            try
            {
                parsedResponse = JsonConvert.DeserializeObject<EspnRosterRequestPayload>(response.Content);
            }
            catch (Exception ex)
            {
                throw new TeamNotFoundException($"Could not retrieve team for this user in ESPN: {ex.Message}");
            }

            var players = parsedResponse?.Team?
                .Select(t => t.Roster)
                .SelectMany(r => r.PlayerEntries)
                .Select(pe => pe.PlayerPoolEntry?.Player)
                .ToList();

            if (players == null || !players.Any())
            {
                throw new PlayersNotFoundException("Could not find any players in ESPN for this user.");
            }

            var playerMapper = new EspnPlayerMapper();

            var mappedPlayers = players
                .Select(p => playerMapper.ConvertPlayer(p))
                .ToList();

            return mappedPlayers;
        }
    }
}