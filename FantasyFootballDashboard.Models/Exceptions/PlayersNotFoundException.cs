using System;

namespace FantasyFootballDashboard.Models.Exceptions
{
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