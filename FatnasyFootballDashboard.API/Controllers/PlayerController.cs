using System.Collections.Generic;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Service;
using Microsoft.AspNetCore.Mvc;

namespace FatnasyFootballDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        [HttpGet]
        [Route("GetPlayers")]
        public IEnumerable<Player> GetPlayers([FromBody]UserProfile userProfile)
        {
            var connectors = ConnectionGenerator.GenerateConnectionsFromUserProfile(userProfile);

            var playerService = new PlayerService(connectors);
            var players = playerService.GetAllUserPlayers();

            return players;
        }
    }
}
