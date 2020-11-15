using System.Linq;
using System.Threading.Tasks;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Service;
using FatnasyFootballDashboard.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FatnasyFootballDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        /// <summary>
        /// Returns a list of players from all services using the provided credentials
        /// </summary>
        /// <param name="userProfile">Object containing the credentials for each service.
        /// For CBS, provide CbsUserName and CbsLeagueName.
        /// For ESPN, provide EspnLeagueId and EspnTeamId.
        /// For MyFantasyLeague, provide MflUsername and MflPassword.
        /// </param>
        /// <returns>List of players from across all services.</returns>
        [HttpGet]
        [Authorize]
        [Route("GetPlayers")]
        public async Task<PlayerPayload> GetPlayers([FromBody]UserProfile userProfile)
        {
            var connectors = ConnectionGenerator.GenerateConnectionsFromUserProfile(userProfile);

            var playerService = new PlayerService(connectors);
            var players = await playerService.GetAllUserPlayers();

            var payload = new PlayerPayload(players.ToList());

            return payload;
        }
    }
}
