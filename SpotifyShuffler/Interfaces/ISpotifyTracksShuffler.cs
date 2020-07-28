using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ISpotifyTracksShuffler
    {
        Task<IOrderedEnumerable<SpotifyTrack>> ShuffleAsync(List<SpotifyTrack> tracks);
    }
}