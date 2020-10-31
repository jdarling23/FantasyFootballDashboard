using FantasyFootballDashboard.APIConnector.CBS;
using FantasyFootballDashboard.APIConnector.Interface;
using FantasyFootballDashboard.ConsoleRunner.TestHarness;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FantasyFootballDashboard.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dependency Injection Container
            var services = new ServiceCollection();

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

                var serviceProvider = services.BuildServiceProvider();

                switch(selection.Trim())
                {
                    case("1"):
                        ConfigureServicesForCbs(services);
                        selectionSuccessfullyMade = true;
                        connector = serviceProvider.GetService<CbsConnector>();
                        tester = new CbsTester();
                        break;
                    case("2"):
                        Console.WriteLine("My Fantsy League is not yet enabled. Please make another selection.");
                        break;
                    case("3"):
                        Console.WriteLine("ESPN is not yet enabled. Please make another selection.");
                        break;
                    default:
                        Console.WriteLine($"{selection} is not valid. Please tyr again.");
                        break;
                }
            }
        
            var testResult = tester.ExecuteTest(connector);
            Console.WriteLine(testResult);
        }

        private static void ConfigureServicesForCbs(ServiceCollection services)
        {
            services
                .AddTransient<IConnector, CbsConnector>();
        }
    }
}
