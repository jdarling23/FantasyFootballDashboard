using FantasyFootbalDashboard.DBConnector.Interfaces;
using FantasyFootbalDashboard.DBConnector.Models;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.SportsData.Models;
using FantasyFootballDashboard.Models.Enums;
using FantasyFootballDashboard.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyFootballDashboard.Service
{
    /// <summary>
    /// Provides ability to maintain data tables for player reference table.
    /// </summary>
    public class DataService : IDataService
    {
        private readonly IConnector _sportsDataConnector;
        private readonly IReferencePlayerRepository _refPlayerRepo;

        public DataService(IConnector sportsDataCon, IReferencePlayerRepository refPlayerRepo)
        {
            _sportsDataConnector = sportsDataCon;
            _refPlayerRepo = refPlayerRepo;
        }

        /// <summary>
        /// Saves reference player data from SportsData.io into the database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ImportSportsDataReferenceData()
        {
            var rawPlayers = await _sportsDataConnector.GetReferencePlayers();
            var playersToImport = rawPlayers
                .Where(p => !p.Status.Equals("Inactive"))
                .Where(p => !p.Position.Equals("OL"))
                .Where(p => !p.PositionCategory.Equals("DEF"));

            var existingRefPlayers = _refPlayerRepo.GetAllReferencePlayers();

            var mappedPlayers = playersToImport
                .Select(p => MapReferencePlayer(
                    (SportsDataPlayer)p, 
                    existingRefPlayers
                        .FirstOrDefault(rp => rp.SportsDataIoPlayerId == p.PlayerId)))
                .ToList();

            try
            {
                _refPlayerRepo.SavePlayers(mappedPlayers);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private ReferencePlayer MapReferencePlayer(SportsDataPlayer rawPlayer, ReferencePlayer refPlayer = null)
        {
            if (refPlayer == null)
            {
                return new ReferencePlayer
                {
                    Name = rawPlayer.Name,
                    Position = (int)PositionMapper.MapPosition(rawPlayer.Position),
                    Team = (int)TeamMapper.MapTeam(rawPlayer.Team),
                    PlayerPhotoUrl = rawPlayer.PhotoUrl,
                    SportsDataIoPlayerId = rawPlayer.PlayerId,
                    Status = rawPlayer.Status,
                    JerseyNumber = rawPlayer.JerseyNumber ?? 0,
                    YearsInLeague = rawPlayer.YearsInLeague ?? 0,
                    AverageDraftPosition = rawPlayer.AverageDraftPosition ?? 0,
                    ByeWeek = rawPlayer.ByeWeek ?? 0,
                    College = rawPlayer.College,
                    Active = rawPlayer.Status.Equals("Active")
                };
            }

            refPlayer.Name = rawPlayer.Name;
            refPlayer.Position = (int)PositionMapper.MapPosition(rawPlayer.Position);
            refPlayer.Team = (int)TeamMapper.MapTeam(rawPlayer.Team);
            refPlayer.PlayerPhotoUrl = rawPlayer.PhotoUrl;
            refPlayer.Status = rawPlayer.Status;
            refPlayer.JerseyNumber = rawPlayer.JerseyNumber ?? 0;
            refPlayer.YearsInLeague = rawPlayer.YearsInLeague ?? 0;
            refPlayer.AverageDraftPosition = rawPlayer.AverageDraftPosition ?? 0;
            refPlayer.ByeWeek = rawPlayer.ByeWeek ?? 0;
            refPlayer.Active = rawPlayer.Status.Equals("Active");

            return refPlayer;
        }
    }
}
