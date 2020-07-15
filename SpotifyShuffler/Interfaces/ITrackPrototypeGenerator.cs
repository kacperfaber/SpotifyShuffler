using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ITrackPrototypeGenerator
    {
        Task<TrackPrototype> GenerateAsync(SpotifyTrack track);
    }
}