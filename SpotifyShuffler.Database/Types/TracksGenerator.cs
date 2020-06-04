using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyShuffler.Database.Interfaces;
using SpotifyShuffler.Database.Models;
using SpotifyShuffler.Interface;

namespace SpotifyShuffler.Database.Types
{
    public class TracksGenerator : ITracksGenerator
    {
        public ITrackGenerator TrackGenerator;

        public TracksGenerator(ITrackGenerator trackGenerator)
        {
            TrackGenerator = trackGenerator;
        }

        public IEnumerable<Track> GenerateTracks(SpotifyPlaylist playlist)
        {
            foreach (PlaylistTrackObject trackObject in playlist.Tracks.Items)
            {
                yield return TrackGenerator.GenerateTrack(trackObject.Track);
            }
        }
    }
}