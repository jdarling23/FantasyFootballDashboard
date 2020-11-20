using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootbalDashboard.DBConnector.Models
{
    /// <summary>
    /// Model for Players in the database. This table is meant to fill in the gaps for missing data from the 
    /// Fatnasy Football service APIs.
    /// </summary>
    public class ReferencePlayer
    {
        [Key]
        public Guid ReferencePlayerId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public int Team { get; set; }

        public string PlayerPhotoUrl { get; set; }

        public int? SportsDataIoPlayerId { get; set; }

        public int? EspnPlayerId { get; set; }

        public int? MyFantasyLeagePlayerId { get; set; }

        public bool Active { get; set; } = true;
    }
}
