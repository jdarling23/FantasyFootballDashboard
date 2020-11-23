using System;
using System.Threading.Tasks;
using FantasyFootballDashboard.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FatnasyFootballDashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IDataService _dataService;
        private readonly ILogger<DataController> _logger;

        public DataController(
            IConfiguration config,
            IDataService dataService,
            ILogger<DataController> logger
        )
        {
            _config = config;
            _dataService = dataService;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint to call to import reference player data into the database from SportsData.io
        /// </summary>
        /// <param name="providedPassword">Password to make this available but still locked down</param>
        /// <returns>Message</returns>
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("ImportPlayerData")]
        public async Task<IActionResult> ImportPlayerData([FromBody] string providedPassword)
        {
            if (!providedPassword.Equals(_config["DataImportPassword"]))
            {
                _logger.LogWarning($"Incorrect password given to import data: {providedPassword}");
                return Unauthorized("Provided password was incorrect.");
            }

            try
            {
                var result = await _dataService.ImportSportsDataReferenceData();
                if (result)
                {
                    return Ok("Player data was sucessfully imported to the database.");
                }

                return StatusCode(500, "There was an error importing the player referernce data.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error importing player data: {ex.Message}");
                return StatusCode(500, "There was an error importing the player referernce data.");
            }
        }
    }
}
