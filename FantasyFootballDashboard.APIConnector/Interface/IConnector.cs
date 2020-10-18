using FantasyFootballDashboard.APIConnector.Models;

namespace FantasyFootballDashboard.APIConnector.Interface
{
	/// <summary>
	/// Interface for an API connection
	/// </summary>
	public interface IConnector
	{
		/// <summary>
		/// Gets a token from an API provider
		/// </summary>
		/// <param name="userName">Username to access provider's system</param>
		/// <param name="password">Password to access account</param>
		/// <param name="targetUrl">URL to send credentials to in order to receive token</param>
		/// <returns>Access Token</returns>
		public AccessToken GetToken(string userName, string password, string targetUrl);
	}
}