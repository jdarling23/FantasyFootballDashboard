using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Service;
using FatnasyFootballDashboard.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FatnasyFootballDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns a list of players from all services using the provided credentials
        /// </summary>
        /// <param name="userProfile">Object containing the credentials for each service.
        /// For CBS, provide CbsUserName and CbsLeagueName.
        /// For ESPN, provide EspnLeagueId and EspnTeamId.
        /// For MyFantasyLeague, provide MflUsername and MflPassword.
        /// </param>
        /// <returns>List of players from across all services.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerPayload), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        //[Authorize]
        [Route("Players")]
        public async Task<IActionResult> ReturnPlayers([FromBody]UserProfile userProfile)
        {
            if (userProfile == null)
            {
                return BadRequest("Provided User Profile object was null");
            }

            var connectors = new List<IConnector>();

            try
            {
                connectors = ConnectionGenerator.GenerateConnectionsFromUserProfile(userProfile, _logger);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error connecting to services; HTTP request returned 500: {ex.Message}");
                return StatusCode(500, "There was an error generating a connection to the desired Fantasy Football services.");
            }

            try
            {
                var playerService = new PlayerService(connectors);
                var players = await playerService.GetAllUserPlayers();

                var payload = new PlayerPayload(players.ToList());

                return Ok(payload);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error getting players; HTTP Request returned 500: {ex.Message}");
                return StatusCode(500, "There was an issue getting the players from the requested Fantasy Football services.");
            }
        }
    }
}
