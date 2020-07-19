using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class SpotifyUrisGenerator : ISpotifyUrisGenerator
    {
        public IEnumerable<string> Generate(IOrderedEnumerable<TrackPrototype> prototypes)
        {
            foreach (TrackPrototype prototype in prototypes)
            {
                yield return prototype.SpotifyUri;
            }
        }
    }
}