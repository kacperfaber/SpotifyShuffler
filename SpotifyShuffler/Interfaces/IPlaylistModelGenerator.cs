using SpotifyShuffler.Interface;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Interfaces
{
    public interface IPlaylistModelGenerator
    {
        PlaylistModel Generate(SimpleSpotifyPlaylist playlist);
    }
}