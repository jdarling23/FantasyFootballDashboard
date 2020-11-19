using System;

namespace FantasyFootballDashboard.Models.Exceptions
{
    /// <summary>
    /// Exception denoting that a connection could not be made to a given Fantasy Footbal service
    /// </summary>
    public class ServiceLoginException : Exception
    {
        public ServiceLoginException()
        {

        }

        public ServiceLoginException(string message)
            : base(message)
        {

        }

        public ServiceLoginException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
