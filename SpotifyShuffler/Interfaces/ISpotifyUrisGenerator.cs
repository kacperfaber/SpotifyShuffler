using System.Collections.Generic;
using System.Linq;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface ISpotifyUrisGenerator
    {
        IEnumerable<string> Generate(IOrderedEnumerable<TrackPrototype> prototypes);
    }
}