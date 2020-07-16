using System.Collections.Generic;

namespace SpotifyShuffler.Interface
{
    public interface ITrackUriGenerator
    {
        IEnumerable<string> Generate(params SimpleSpotifyTrack[] tracks);
    }
}