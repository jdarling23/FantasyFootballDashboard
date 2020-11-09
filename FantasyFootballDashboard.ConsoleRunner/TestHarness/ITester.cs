using System.Threading.Tasks;

namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public interface ITester
    {
        Task<string> ExecuteTest();
    }

    public class DefaultTester : ITester
    {
        public Task<string> ExecuteTest()
        {
            return Task.FromResult("What is my purpose? You pass butter. Oh my god.");
        }
    }
}