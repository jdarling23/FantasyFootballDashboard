using System;

namespace FantasyFootballDashboard.Models.Exceptions
{
	/// <summary>
	/// Exception denoting that a player could not be found in a target Fantasy Football service
	/// </summary>
	public class PlayersNotFoundException : Exception
	{
		public PlayersNotFoundException()
		{

		}

		public PlayersNotFoundException(string message)
			: base(message)
		{

		}

		public PlayersNotFoundException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}
}