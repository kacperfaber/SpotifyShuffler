using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Database.Interfaces
{
    public interface ITrackGenerator
    {
        Track GenerateTrack(SpotifyTrack spotifyTrack);
    }
}