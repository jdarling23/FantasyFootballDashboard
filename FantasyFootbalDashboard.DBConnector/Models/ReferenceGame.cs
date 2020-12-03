using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootbalDashboard.DBConnector.Models
{
    /// <summary>
    /// Object representing an NFL Game to be stored in the database
    /// </summary>
    public class ReferenceGame
    {
        [Key]
        public int GameId { get; set; }

        public int SeasonYear { get; set; }

        public int AwayTeam { get; set; }

        public int HomeTeam { get; set; }

        public int Week { get; set; }

        public string NetworkChannel { get; set; }

        public DateTime? GameDateTime { get; set; }
    }
}
