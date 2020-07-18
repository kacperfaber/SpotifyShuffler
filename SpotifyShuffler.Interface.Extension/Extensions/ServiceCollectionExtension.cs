using Microsoft.Extensions.DependencyInjection;

namespace SpotifyShuffler.Interface.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddSpotify(this IServiceCollection collection)
        {
            collection.AddScoped<SpotifyService>();
        }
    }
}