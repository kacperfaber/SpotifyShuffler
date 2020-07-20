using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ITrackPrototypesGenerator
    {
        Task<List<TrackPrototype>> GenerateAsync(IEnumerable<SpotifyTrack> tracks);
    }
}