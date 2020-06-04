using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Database.Interfaces
{
    public interface IPrimaryArtistGenerator
    {
        PrimaryArtist GeneratePrimaryArtist(SimpleSpotifyArtist[] artists);
    }
}