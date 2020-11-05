using System;

namespace FantasyFootballDashboard.Models.Exceptions
{
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
