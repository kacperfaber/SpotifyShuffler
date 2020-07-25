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

        public async Task<List<TrackPrototype>> GenerateAsync(IEnumerable<SpotifyTrack> tracks)
        {
            List<TrackPrototype> prototypes = new List<TrackPrototype>();

            foreach (SpotifyTrack track in tracks)
            {
                TrackPrototype proto = await PrototypeGenerator.GenerateAsync(track);
                prototypes.Add(proto);
            }

            return prototypes;
        }
    }
}