using System;

namespace FantasyFootballDashboard.Models.Exceptions
{
	/// <summary>
	/// Custom exception thrown when data on an NFL game cannot be found.
	/// </summary>
    public class GamesNotFoundException : Exception
    {
		public GamesNotFoundException()
		{

		}

		public GamesNotFoundException(string message)
			: base(message)
		{

		}

		public GamesNotFoundException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}
}
