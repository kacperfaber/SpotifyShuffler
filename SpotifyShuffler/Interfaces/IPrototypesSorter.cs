using System.Collections.Generic;
using SpotifyShuffler.Database;

namespace SpotifyShuffler.Interfaces
{
    public interface IPrototypesSorter
    {
        void Sort(ref IEnumerable<TrackPrototype> prototypes);
    }
}