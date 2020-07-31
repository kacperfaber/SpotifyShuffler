using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Models;

namespace SpotifyShuffler.Types
{
    public class TrackModelGenerator : ITrackModelGenerator
    {
        public IArtistLabelGenerator ArtistLabelGenerator;

        public TrackModelGenerator(IArtistLabelGenerator artistLabelGenerator)
        {
            ArtistLabelGenerator = artistLabelGenerator;
        }

        public TrackModel Generate(SpotifyTrack track)
        {
            return new TrackModel
            {
                SpotifyTrack = track,
                ArtistLabel = ArtistLabelGenerator.Generate(track.Artists)
            };
        }
    }
}