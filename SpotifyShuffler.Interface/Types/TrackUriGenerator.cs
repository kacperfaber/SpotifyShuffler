using System.Collections.Generic;
using System.Text;

namespace SpotifyShuffler.Interface
{
    public class TrackUriGenerator : ITrackUriGenerator
    {
        public IEnumerable<string> Generate(params SimpleSpotifyTrack[] tracks)
        {
            foreach (SimpleSpotifyTrack t in tracks)
            {
                yield return t.Uri;
            }
        }
    }
}