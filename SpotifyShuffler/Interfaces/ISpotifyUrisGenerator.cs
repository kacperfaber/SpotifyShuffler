using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ISpotifyUrisGenerator
    {
        IEnumerable<string> Generate(List<TrackPrototype> prototypes);
    }
}