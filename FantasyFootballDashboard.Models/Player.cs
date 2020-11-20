using FantasyFootballDashboard.Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyFootballDashboard.Models
{
	/// <summary>
	/// Data model for representing players in the Fantasy Football Dashboard
	/// </summary>
	public class Player
	{
		/// <summary>
		/// Full name of a player
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Position played by this player
		/// </summary>
        public Position Position { get; set; }

		/// <summary>
		/// Current team the player plays for
		/// </summary>
        public NflTeam Team { get; set; }

		/// <summary>
		/// A list of the fantasy football services where you own this player
		/// </summary>
		public List<ServiceOption> Service { get; set; } = new List<ServiceOption>();

		/// <summary>
		/// Map what ID a player is across Fantasy Football services
		/// </summary>
		[JsonIgnore]
		public Dictionary<ServiceOption, int> ServiceIDs { get; set; }
    }
}