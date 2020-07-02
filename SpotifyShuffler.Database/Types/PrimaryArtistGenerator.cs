using System.Linq;
using SpotifyShuffler.Database.Interfaces;
using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Database.Types
{
    public class PrimaryArtistGenerator : IPrimaryArtistGenerator
    {
        public PrimaryArtist GeneratePrimaryArtist(SimpleSpotifyArtist[] artists)
        {
            SimpleSpotifyArtist artist = artists.First();

            return new PrimaryArtist
            {
                Name = artist.Name,
                SpotifyId = artist.Id
            };
        }
    }
}