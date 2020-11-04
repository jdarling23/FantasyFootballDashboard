namespace FantasyFootballDashboard.ConsoleRunner.TestHarness
{
    public interface ITester
    {
        string ExecuteTest();
    }

    public class DefaultTester : ITester
    {
        public string ExecuteTest()
        {
            return "What is my purpose? You pass butter. Oh my god.";
        }
    }
}