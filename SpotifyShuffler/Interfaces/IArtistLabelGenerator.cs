using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface IArtistLabelGenerator
    {
        string Generate(SimpleSpotifyArtist[] artists);
    }
}