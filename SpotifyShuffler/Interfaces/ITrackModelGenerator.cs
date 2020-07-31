using SpotifyShuffler.Interface;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Interfaces
{
    public interface ITrackModelGenerator
    {
        TrackModel Generate(SpotifyTrack track);
    }
}