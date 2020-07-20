using System.Linq;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class OrderedPrototypesProvider : IOrderedPrototypesProvider
    {
        public IOrderedEnumerable<TrackPrototype> Provide(PlaylistPrototype prototype)
        {
            return prototype.Tracks.OrderBy(x => x.Index);
        }
    }
}