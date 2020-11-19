using System;

namespace FantasyFootballDashboard.Models.Exceptions
{
	/// <summary>
	/// Exception denoting that a requested team could not be located in a given Fantasy Fooball service
	/// </summary>
	public class TeamNotFoundException : Exception
	{
		public TeamNotFoundException()
		{

		}

		public TeamNotFoundException(string message)
			: base(message)
		{

		}

		public TeamNotFoundException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}
}