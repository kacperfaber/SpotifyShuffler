using System.Collections.Generic;
using System.Linq;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Interfaces
{
    public interface ISpotifyUrisGenerator
    {
        IEnumerable<string> Generate(IOrderedEnumerable<SpotifyTrack> tracks);
    }
}