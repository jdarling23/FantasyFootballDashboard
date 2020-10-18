using System;

namespace FantasyFootballDashboard.APIConnector.Exceptions
{
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