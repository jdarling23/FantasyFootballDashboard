using FantasyFootballDashboard.ConsoleRunner.TestHarness;
using System;

namespace FantasyFootballDashboard.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var testResult = CbsTester.ExecuteTest();
            Console.WriteLine(testResult);
        }
    }
}
