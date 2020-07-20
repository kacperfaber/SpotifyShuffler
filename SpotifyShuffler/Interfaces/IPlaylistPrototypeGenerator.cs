using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface IPlaylistPrototypeGenerator
    {
        Task<PlaylistPrototype> GenerateAsync(IEnumerable<SpotifyTrack> tracks, Operation operation);
    }
}