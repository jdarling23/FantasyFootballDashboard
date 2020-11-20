using FantasyFootballDashboard.Models.Enums;

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

    }
}