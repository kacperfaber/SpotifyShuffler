using System.Collections.Generic;
using System.Linq;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class SpotifyUrisGenerator : ISpotifyUrisGenerator
    {
        public IEnumerable<string> Generate(IOrderedEnumerable<SpotifyTrack> tracks)
        {
            foreach (SpotifyTrack track in tracks)
            {
                yield return track.Uri;
            }
        }
    }
}