using FantasyFootballDashboard.APIConnector.CBS;
using FantasyFootballDashboard.APIConnector.Interface;
using FantasyFootballDashboard.ConsoleRunner.TestHarness;
using System;
using System.Security.Cryptography;

namespace FantasyFootballDashboard.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prepare default objects
            IConnector connector = new DefaultConnector();
            ITester tester = new DefaultTester();

            // Set Service to Test
            var selectionSuccessfullyMade = false;

            while(!selectionSuccessfullyMade)
            {
                Console.WriteLine("Welcome to the Test Harness! Please select a service from the following options: ");
                Console.WriteLine("For CBS Fantasy Sports, enter 1.");
                Console.WriteLine("For My Fantasy League, enter 2.");
                Console.WriteLine("For ESPN Fantasy Sports, enter 3.");

                var selection = Console.ReadLine();

                switch(selection.Trim())
                {
                    case("1"):
                        selectionSuccessfullyMade = true;
                        var cbsCredentials = GetCbsCredentials();
                        connector = new CbsConnector(cbsCredentials.league, cbsCredentials.username);
                        tester = new CbsTester(connector);
                        break;
                    case("2"):
                        Console.WriteLine("My Fantsy League is not yet enabled. Please make another selection.");
                        break;
                    case("3"):
                        Console.WriteLine("ESPN is not yet enabled. Please make another selection.");
                        break;
                    default:
                        Console.WriteLine($"{selection} is not valid. Please try again.");
                        break;
                }
            }

            try
            {
                var testResult = tester.ExecuteTest();
                Console.WriteLine(testResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encountered: {ex.Message}");
            }

            Console.WriteLine("Ending application, please click any key...");
            Console.ReadLine();
        }

        private static (string league, string username) GetCbsCredentials()
        {
            Console.WriteLine("Enter CBS League Name: ");
            var inputLeague = Console.ReadLine ();

            Console.WriteLine("Enter CBS Username: ");
            var inputUsername = Console.ReadLine();

            return (league: inputLeague, username: inputUsername);
        }
    }
}
