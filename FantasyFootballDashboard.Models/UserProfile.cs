using System;

namespace FantasyFootballDashboard.Models
{
    /// <summary>
    /// Object containing credentials to log into target Fantasy Football services
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Year of the target NFL season
        /// </summary>
        public string Year { get; set; } = DateTime.Now.Year.ToString();

        /// <summary>
        /// Username to log into CBS Fantasy Football service
        /// </summary>
        public string CbsUsername { get; set; }

        /// <summary>
        /// Name of the league containing the target user team in the CBS Fantasy Footbal service
        /// </summary>
        public string CbsLeagueName { get; set; }

        /// <summary>
        /// ID of the league containing the target user team in the ESPN Fantasy Football service.
        /// This ID can be found in the URL of the ESPN Fantasy Football website once a user logs in.
        /// There is unfortuantely no way to programtically find this value at this time.
        /// </summary>
        public string EspnLeagueId { get; set; }

        /// <summary>
        /// ID of the user team in the ESPN Fantasy Football service.
        /// This ID can be found in the URL of the ESPN Fantasy Football website once a user logs in.
        /// There is unfortuantely no way to programtically find this value at this time.
        /// </summary>
        public string EspnTeamId { get; set; }

        /// <summary>
        /// Username to log into My Fantasy League Fantasy Football service
        /// </summary>
        public string MflUsername { get; set; }

        /// <summary>
        /// User password to log into My Fantasy League Fantasy Football service
        /// </summary>
        public string MflPassword { get; set; }
    }
}
