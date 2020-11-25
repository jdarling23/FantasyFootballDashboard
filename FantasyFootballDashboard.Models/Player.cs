using FantasyFootballDashboard.Models.Collections;
using FantasyFootballDashboard.Models.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FantasyFootballDashboard.Models
{
	/// <summary>
	/// Data model for representing players in the Fantasy Football Dashboard
	/// </summary>
	public class Player : IDataObject
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
		public Dictionary<ServiceOption, int> ServiceIDs { get; set; } = new Dictionary<ServiceOption, int>();

		/// <summary>
		/// Internal key used in uniqueness checks
		/// </summary>
		[JsonIgnore]
        public string Key => $"{Name}";

		/// <summary>
		/// Copies in service data from another player
		/// </summary>
		/// <param name="obj">Player to copy</param>
		/// <returns>True if copy successful</returns>
		public bool Copy(object obj)
        {
			if (obj is Player)
			{
				Player copyPlayer = obj as Player;

				foreach(var service in copyPlayer.Service)
                {
					if (!Service.Contains(service))
                    {
						Service.Add(service);
                        try
                        {
                            var serviceId = copyPlayer.ServiceIDs[service];
							ServiceIDs.Add(service, serviceId);
                        }
                        catch
                        {
							continue;
                        }					
					}
                }

				return true;
			}
			return false;
		}
    }
}