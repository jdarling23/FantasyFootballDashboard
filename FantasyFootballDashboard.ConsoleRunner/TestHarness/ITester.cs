using FantasyFootballDashboard.APIConnector.Interface;
using System;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public interface ITester
    {
        string ExecuteTest(IConnector conn);
    }

    public class DefaultTester : ITester
    {
        public string ExecuteTest(IConnector conn)
        {
            return "What is my purpose? You pass butter. Oh my god.";
        }
    }
}