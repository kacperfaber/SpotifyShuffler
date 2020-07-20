using System.Linq;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IOrderedPrototypesProvider
    {
        IOrderedEnumerable<TrackPrototype> Provide(PlaylistPrototype prototype);
    }
}