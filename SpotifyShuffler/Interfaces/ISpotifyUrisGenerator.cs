using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ISpotifyUrisGenerator
    {
        IEnumerable<string> Generate(IOrderedEnumerable<TrackPrototype> prototypes);
    }
}