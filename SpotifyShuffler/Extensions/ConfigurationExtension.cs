using System;
using Microsoft.Extensions.Configuration;

namespace SpotifyShuffler
{
    public static class ConfigurationExtension
    {
        public static bool IsDevelopment(this IConfiguration configuration)
        {
            return Environment.GetEnvironmentVariable("ENVIRONMENT").Equals("Environment", StringComparison.InvariantCultureIgnoreCase);
        }
        
        public static bool IsProduction(this IConfiguration configuration)
        {
            return Environment.GetEnvironmentVariable("ENVIRONMENT").Equals("Production", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}