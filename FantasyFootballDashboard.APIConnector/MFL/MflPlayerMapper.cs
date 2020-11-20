using FantasyFootballDashboard.APIConnector.MFL.Models;
using FantasyFootballDashboard.Models;
using FantasyFootballDashboard.Models.Enums;

namespace FantasyFootballDashboard.APIConnector.MFL
{
    /// <summary>
    /// Helper class to map player data from My Fantasy League to the system player model
    /// </summary>
    public class MflPlayerMapper
    {
        /// <summary>
        /// Maps My Fantasy League player data to system player object
        /// </summary>
        /// <param name="player">MFL player object</param>
        /// <returns>System player object</returns>
        public Player ConvertPlayer(MflPlayer player)
        {
            var mappedPlayer = new Player();

            mappedPlayer.Name = player.FullName;
            mappedPlayer.Position = MapPosition(player.Position);
            mappedPlayer.Team = MapTeam(player.NflTeam);
            mappedPlayer.Service.Add(ServiceOption.MyFantasyLeague);
            mappedPlayer.ServiceIDs.Add(ServiceOption.MyFantasyLeague, int.Parse(player.PlayerId));

            return mappedPlayer;
        }

        private Position MapPosition(string position)
        {
            switch (position)
            {
                case ("QB"):
                    return Position.QuarterBack;
                case ("RB"):
                    return Position.RunningBack;
                case ("WR"):
                    return Position.WideReceiver;
                case ("TE"):
                    return Position.TightEnd;
                case ("Def"):
                    return Position.DefenseSpecialTeams;
                case ("PK"):
                    return Position.Kicker;
                default:
                    return Position.None;
            }
        }

        private NflTeam MapTeam(string team)
        {
            switch (team)
            {
                case ("ARI"):
                    return NflTeam.ArizonaCardinals;
                case ("ATL"):
                    return NflTeam.AtlantaFalcons;
                case ("BAL"):
                    return NflTeam.BalitmoreRavens;
                case ("BUF"):
                    return NflTeam.BuffaloBills;
                case ("CAR"):
                    return NflTeam.CarolinaPanthers;
                case ("CHI"):
                    return NflTeam.ChicagoBears;
                case ("CIN"):
                    return NflTeam.CincinnatiBengals;
                case ("CLE"):
                    return NflTeam.ClevelandBrowns;
                case ("DAL"):
                    return NflTeam.DallasCowboys;
                case ("DEN"):
                    return NflTeam.DenverBroncos;
                case ("DET"):
                    return NflTeam.DetroitLions;
                case ("GBP"):
                    return NflTeam.GreenBayPackers;
                case ("HOU"):
                    return NflTeam.HoustonTexans;
                case ("IND"):
                    return NflTeam.IndianapolisColts;
                case ("JAX"):
                    return NflTeam.JacksonvilleJaguars;
                case ("KCC"):
                    return NflTeam.KansasCityCheifs;
                case ("LVR"):
                    return NflTeam.LasVegasRaiders;
                case ("LAC"):
                    return NflTeam.LosAngelesChargers;
                case ("LAR"):
                    return NflTeam.LosAngelesRams;
                case ("MIA"):
                    return NflTeam.MiamiDolphins;
                case ("MIN"):
                    return NflTeam.MinnesotaVikings;
                case ("NEP"):
                    return NflTeam.NewEnglandPatriots;
                case ("NOS"):
                    return NflTeam.NewOrleansSaints;
                case ("NYG"):
                    return NflTeam.NewYorkGiants;
                case ("NYJ"):
                    return NflTeam.NewYorkJets;
                case ("PHI"):
                    return NflTeam.PhilidelphiaEagles;
                case ("PIT"):
                    return NflTeam.PittsburghSteelers;
                case ("SFF"):
                    return NflTeam.SanFrancisco49ers;
                case ("SEA"):
                    return NflTeam.SeattleSeahawks;
                case ("TBB"):
                    return NflTeam.TampaBayBuccaneers;
                case ("TEN"):
                    return NflTeam.TenneseeTitans;
                case ("WAS"):
                    return NflTeam.WashingtonFootballTeam;
                default:
                    return NflTeam.FreeAgent;
            }
        }
    }
}
