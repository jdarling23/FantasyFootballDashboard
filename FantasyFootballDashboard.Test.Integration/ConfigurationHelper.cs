using FantasyFootballDashboard.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FantasyFootballDashboard.Test.Integration
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets("c81a3861-5c57-4b35-9e46-2753672ca3e7")
                .Build();
        }

        public static UserProfile GetTestProfile()
        {
            var configuration = new UserProfile();

            var iConfig = GetIConfigurationRoot();

            ConfigurationBinder.Bind(iConfig, configuration);

            return configuration;
        }

        public static string GetSportsDataApiKey()
        {
            var iConfig = GetIConfigurationRoot();
            return iConfig["SportsDataKey"];
        }
    }
}
