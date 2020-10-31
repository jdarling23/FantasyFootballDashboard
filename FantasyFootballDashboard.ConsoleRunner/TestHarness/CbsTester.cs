using FantasyFootballDashboard.APIConnector.CBS;
using FantasyFootballDashboard.APIConnector.Interface;
using System;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public class CbsTester : ITester
    {
        public string ExecuteTest(IConnector conn)
        {
            return "CBS Test was successful!";
        }
    }
}