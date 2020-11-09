using FantasyFootballDashboard.APIConnector.CBS;
using FantasyFootballDashboard.APIConnector.ESPN;
using FantasyFootballDashboard.APIConnector.Interfaces;
using FantasyFootballDashboard.APIConnector.MFL;
using FantasyFootballDashboard.ConsoleRunner.TestHarness;
using System;

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
                try
                {
                    switch (selection.Trim())
                    {
                        case ("1"):
                            selectionSuccessfullyMade = true;
                            var cbsCredentials = GetCbsCredentials();
                            connector = new CbsConnector(cbsCredentials.league, cbsCredentials.username);
                            tester = new CbsTester(connector);
                            break;
                        case ("2"):
                            selectionSuccessfullyMade = true;
                            var mflCredentials = GetMflCrednetials();
                            connector = new MflConnector(mflCredentials.year, mflCredentials.username, mflCredentials.password);
                            tester = new MflTester(connector);
                            break;
                        case ("3"):
                            selectionSuccessfullyMade = true;
                            var espnCrednetials = GetEspnCredentials();
                            connector = new EspnConnector(espnCrednetials.year, espnCrednetials.leagueId, espnCrednetials.teamId);
                            tester = new EspnTester(connector);
                            break;
                        default:
                            Console.WriteLine($"{selection} is not valid. Please try again.");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    selectionSuccessfullyMade = false;
                    Console.WriteLine($"Error configuring selection: {ex.Message}");
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

        private static (string year, string leagueId, string teamId) GetEspnCredentials()
        {
            Console.WriteLine("Enter League Year: ");
            var inputYear = Console.ReadLine();

            Console.WriteLine("Enter ESPN League ID: ");
            var inputLeagueId = Console.ReadLine();

            Console.WriteLine("Enter ESPN Team ID: ");
            var inputTeamId = Console.ReadLine();

            return (year: inputYear, leagueId: inputLeagueId, teamId: inputTeamId);
        }

        private static (string year, string username, string password) GetMflCrednetials()
        {
            Console.WriteLine("Enter MFL League Year: ");
            var inputYear = Console.ReadLine();

            Console.WriteLine("Enter MFL Username: ");
            var inputUsername = Console.ReadLine();

            // This leverages some fun stuff to obscure the password entry
            Console.WriteLine("Enter MFL Password: ");
            ConsoleKeyInfo keyInfo;
            string inputPassword = string.Empty;
            do
            {
                keyInfo = Console.ReadKey(true);
                // Skip if Backspace or Enter is Pressed
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    inputPassword += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (keyInfo.Key == ConsoleKey.Backspace && inputPassword.Length > 0)
                    {
                        // Remove last charcter if Backspace is Pressed
                        inputPassword = inputPassword.Substring(0, (inputPassword.Length - 1));
                        Console.Write("b b");
                    }
                }
            }
            // Stops Getting Password Once Enter is Pressed
            while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine(string.Empty);

            return (year: inputYear, username: inputUsername, password: inputPassword);
        }
    }
}
