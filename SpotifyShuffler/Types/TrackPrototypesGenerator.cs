using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
using SpotifyShuffler.Interfaces;

namespace SpotifyShuffler.Types
{
    public class TrackPrototypesGenerator : ITrackPrototypesGenerator
    {
        public ITrackPrototypeGenerator PrototypeGenerator;

        public TrackPrototypesGenerator(ITrackPrototypeGenerator prototypeGenerator)
        {
            PrototypeGenerator = prototypeGenerator;
        }

        public async Task<List<TrackPrototype>> GenerateAsync(SpotifyPlaylist playlist)
        {
            List<TrackPrototype> prototypes = new List<TrackPrototype>();
            
            foreach (PlaylistTrackObject trackObject in playlist.Tracks.Items)
            {
                TrackPrototype proto = await PrototypeGenerator.GenerateAsync(trackObject.Track);
                prototypes.Add(proto);
            }

            return prototypes;
        }
    }
}