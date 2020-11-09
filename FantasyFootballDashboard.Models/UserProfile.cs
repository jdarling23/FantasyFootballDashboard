using System;

namespace FantasyFootballDashboard.Models
{
    public class UserProfile
    {
        public string Year { get; set; } = DateTime.Now.Year.ToString();

        public string CbsUsername { get; set; }

        public string CbsLeagueName { get; set; }

        public string EspnLeagueId { get; set; }

        public string EspnTeamId { get; set; }

        public string MflUsername { get; set; }

        public string MflPassword { get; set; }
    }
}
